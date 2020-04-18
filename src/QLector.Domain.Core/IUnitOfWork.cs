using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Domain.Core
{
    /// <summary>
    /// A database Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Commit(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Rollback changes
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Rollback(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets ID of current operation
        /// </summary>
        Guid OperationId { get; }
    }
}
