using System.Reflection;

namespace QLector.Application.Core.Infrastructure
{
    internal class HostAssemblyProvider : IHostAssemblyProvider
    {
        public Assembly GetEntryAssembly() => Assembly.GetEntryAssembly();
    }
}
