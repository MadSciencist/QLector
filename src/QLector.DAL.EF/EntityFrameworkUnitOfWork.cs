using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QLector.Domain.Core;
using System;
using System.Threading.Tasks;

namespace QLector.DAL.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private Guid _transactionId;
        public Guid TransactionId { get => _transactionId; }

        private readonly ILogger<EntityFrameworkUnitOfWork> _logger;
        private readonly AppDbContext _dbContex;

        public EntityFrameworkUnitOfWork(AppDbContext context, ILogger<EntityFrameworkUnitOfWork> logger)
        {
            _dbContex = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _transactionId = Guid.NewGuid();
        }

        public async Task Commit()
        {
            try
            {
                await _dbContex.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, nameof(Commit));
                throw;
            }
        }

        public async Task Rollback()
        {
            try
            {
                await DoRollback();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Rollback));
            }
        }

        private Task DoRollback()
        {
            // TODO check this
            foreach (var entry in _dbContex.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        entry.Reload();
                        break;

                    default:
                        break;
                }
            }

            return Task.FromResult(0);
        }
    }
}
