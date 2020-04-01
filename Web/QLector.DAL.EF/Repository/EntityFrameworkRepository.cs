using Microsoft.EntityFrameworkCore;
using QLector.Domain.Abstractions;
using QLector.Entities.Entity;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository
{
    public class EntityFrameworkRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity
    {
        protected readonly DbContext Context;

        public EntityFrameworkRepository(DbContext context)
        {
            Context = context;
        }

        public virtual async Task<TEntity> FindById(TKey id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await Context.AddAsync(entity);
            return entity;
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
    }
}
