using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLector.Application.Core.Behaviors;
using QLector.Application.Core.Infrastructure;
using System;
using System.Linq;
using System.Reflection;

namespace QLector.Application.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        private static Assembly CurrentAssembly => typeof(ModuleLoader).Assembly;

        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config, IHostAssemblyProvider hostAssemblyProvider = null)
        {
            if (hostAssemblyProvider is null)
                hostAssemblyProvider = new HostAssemblyProvider();

            return services
                .AddModules(config, hostAssemblyProvider)
                .AddMediatR(CurrentAssembly)
                .AddAutoMapper(CurrentAssembly)
                .RegisterBehavior(typeof(LoggingBehavior<,>))
                .RegisterBehavior(typeof(AuthorizationBehavior<,>))
                .RegisterBehavior(typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssembly(CurrentAssembly);
        }

        private static IServiceCollection AddModules(this IServiceCollection services, IConfiguration config, IHostAssemblyProvider hostAssemblyProvider)
        {
            var modules = new ModuleLoader(hostAssemblyProvider).GetModules();

            modules.ToList().ForEach(module =>
            {
                services.AddMediatR(module.Assembly);
                services.AddValidatorsFromAssembly(module.Assembly);
                services.AddAutoMapper(module.Assembly);
                services = module.RegisterServices(services, config);
            });

            return services;
        }

        private static IServiceCollection RegisterBehavior(this IServiceCollection services, Type behaviorType)
        {
            if (behaviorType is null)
                throw new ArgumentNullException(nameof(behaviorType));

            return services
                .AddTransient(typeof(IPipelineBehavior<,>), behaviorType);
        }
    }
}
