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
        public Guid OperationId { get; }

        protected readonly ILogger<EntityFrameworkUnitOfWork> Logger;
        protected readonly AppDbContext DbContext;

        public EntityFrameworkUnitOfWork(AppDbContext context, ILogger<EntityFrameworkUnitOfWork> logger)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            OperationId = Guid.NewGuid();
        }

        public async Task Commit(CancellationToken cancellationToken = default(CancellationToken) )
        {
            try
            {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                Logger.LogError(ex, nameof(Commit));
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
                Logger.LogError(ex, nameof(Rollback));
            }
        }

        protected virtual Task DoRollback()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
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
                }
            }

            return Task.CompletedTask;
        }
    }
}
