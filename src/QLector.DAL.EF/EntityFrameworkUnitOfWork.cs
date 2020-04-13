using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QLector.Domain.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.DAL.EF
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public Guid TransactionId { get; }

        private readonly ILogger<EntityFrameworkUnitOfWork> _logger;
        private readonly AppDbContext _dbContext;

        public EntityFrameworkUnitOfWork(AppDbContext context, ILogger<EntityFrameworkUnitOfWork> logger)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            TransactionId = Guid.NewGuid();
        }

        public async Task Commit(CancellationToken cancellationToken = default(CancellationToken) )
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, nameof(Commit));
                throw;
            }
        }

        public async Task Rollback(CancellationToken cancellationToken = default(CancellationToken))
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
            foreach (var entry in _dbContext.ChangeTracker.Entries())
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

            return Task.CompletedTask;
        }
    }
}
