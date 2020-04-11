using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QLector.Application.Core
{
    /// <summary>
    /// Base class for declaring application modules
    /// </summary>
    public abstract class ApplicationModule : IApplicationModule
    {
        public Assembly Assembly => this.GetType().Assembly;

        public virtual IServiceCollection RegisterServices(IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
