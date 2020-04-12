using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLector.Domain.Core
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TEntity> FindById(TKey id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetManyFiltered(Expression<Func<TEntity, bool>> predicate);
    }
}
