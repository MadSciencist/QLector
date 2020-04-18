namespace QLector.Domain.Core
{
    /// <summary>
    /// Base entity class, where the key is integer
    /// </summary>
    public abstract class Entity : Entity<int>
    {
    }

    /// <summary>
    /// Base generic entity class
    /// </summary>
    /// <typeparam name="TKey">Type of the unique identifier</typeparam>
    public abstract class Entity<TKey> : IEntity
    {
        public virtual TKey Id { get; protected set; }
    }
}