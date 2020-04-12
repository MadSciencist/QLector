using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLector.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository
{
    public class EntityFrameworkRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity
    {
        private AppDbContext _dbContext;
        protected AppDbContext Context => _dbContext ??= ServiceProvider.GetRequiredService<AppDbContext>();

        private IUnitOfWork _unitOfWork;
        public IUnitOfWork UnitOfWork => _unitOfWork ??= ServiceProvider.GetRequiredService<IUnitOfWork>();

        protected IServiceProvider ServiceProvider { get; }

        public EntityFrameworkRepository(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public virtual async Task<TEntity> FindById(TKey id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var addResult = await Context.Set<TEntity>().AddAsync(entity);
            return addResult.Entity;
        }

        public virtual Task<TEntity> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public virtual Task Remove(TEntity entity)
        {
            Context.Remove(entity);
            return Task.FromResult(0);
        }

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetManyFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }
    }
}
