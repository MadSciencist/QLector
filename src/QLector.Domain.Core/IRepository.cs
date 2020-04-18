using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLector.Domain.Core
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TKey">Type of the entity's key</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Find entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindById(TKey id);

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Update existing entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Remove(TEntity entity);

        /// <summary>
        /// Find single using predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find multiple entities matching predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindMany(Expression<Func<TEntity, bool>> predicate);
    }
}
