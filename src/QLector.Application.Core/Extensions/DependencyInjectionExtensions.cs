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

        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddModules(config)
                .AddMediatR(CurrentAssembly)
                .AddAutoMapper(CurrentAssembly)
                .RegisterBehavior(typeof(LoggingBehavior<,>))
                .RegisterBehavior(typeof(AuthorizationBehavior<,>))
                .RegisterBehavior(typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssembly(CurrentAssembly);
        }

        private static IServiceCollection AddModules(this IServiceCollection services, IConfiguration config)
        {
            var modules = new ModuleLoader().GetModules();

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
