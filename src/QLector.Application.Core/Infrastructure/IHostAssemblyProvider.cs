using System.Reflection;

namespace QLector.Application.Core.Infrastructure
{
    /// <summary>
    /// Provider of entry assembly
    /// </summary>
    public interface IHostAssemblyProvider
    {
        /// <summary>
        /// Gets the entry assembly
        /// </summary>
        /// <returns></returns>
        Assembly GetEntryAssembly();
    }
}
