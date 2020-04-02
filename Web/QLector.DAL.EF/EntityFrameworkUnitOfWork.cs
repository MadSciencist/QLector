using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QLector.Domain.Abstractions;
using System;
using System.Threading.Tasks;

namespace QLector.DAL.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public Guid TransactionId { get => _transaction.TransactionId; }

        private readonly IDbContextTransaction _transaction;

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public async Task Commit()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // TODO
                throw;
            }
            catch (DbUpdateException ex)
            {
                // TODO
                throw;
            }
        }

        public async Task Rollback()
        {
            await _transaction?.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
