using System.Reflection;

namespace QLector.Application.Core.Infrastructure
{
    public interface IHostAssemblyProvider
    {
        Assembly GetEntryAssembly();
    }
}
