using System;
using System.Threading.Tasks;

namespace QLector.Domain.Core
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task Rollback();
        Guid TransactionId { get; }
    }
}
