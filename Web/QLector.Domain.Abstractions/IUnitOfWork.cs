using System;
using System.Threading.Tasks;

namespace QLector.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}
