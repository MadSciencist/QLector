using System.Threading.Tasks;
using QLector.Entities.Entity;

namespace QLector.Domain.Abstractions
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity
    {
        Task<TEntity> FindById(TKey id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}
