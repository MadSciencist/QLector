using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QLector.Application.Behaviors;
using System;

namespace QLector.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
        {
            return services
                .RegisterMediator()
                .RegisterAutoMapper()
                .RegisterBehavior(typeof(LoggingBehavior<,>))
                //.RegisterBehavior(typeof(AuthorizationBehavior<,>))
                .RegisterBehavior(typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
        }

        private static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(ValidationBehavior<,>).Assembly);
        }

        private static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(ValidationBehavior<,>).Assembly);
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
