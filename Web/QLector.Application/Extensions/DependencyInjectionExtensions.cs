using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QLector.Application.Behaviors;
using System;

namespace QLector.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(ValidationBehavior<,>).Assembly);
        }

        public static IServiceCollection RegisterBehavior(this IServiceCollection services, Type behaviorType)
        {
            if (behaviorType is null)
            {
                throw new ArgumentNullException(nameof(behaviorType));
            }

            services.AddTransient(typeof(IPipelineBehavior<,>), behaviorType);
            
            return services;
        }
    }
}
