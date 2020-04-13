using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Domain.Core
{
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken = default(CancellationToken));
        Task Rollback(CancellationToken cancellationToken = default(CancellationToken));
        Guid TransactionId { get; }
    }
}
