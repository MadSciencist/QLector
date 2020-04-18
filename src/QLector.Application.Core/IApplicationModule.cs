using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QLector.Application.Core
{
    /// <summary>
    /// Represents application module
    /// </summary>
    public interface IApplicationModule
    {
        /// <summary>
        /// Module assembly
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Dependency injection configuration
        /// Mappers, validators and MediatR are registered automatically.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        IServiceCollection RegisterServices(IServiceCollection services, IConfiguration config);
    }
}
