namespace QLector.Domain.Core
{
    /// <summary>
    /// Generic entity
    /// </summary>
    /// <typeparam name="TKey">Type of the unique identifier</typeparam>
    public interface IEntity<out TKey> : IEntity
    {
        TKey Id { get; }
    }

    /// <summary>
    /// Entity marker interface
    /// </summary>
    public interface IEntity
    {
    }
}