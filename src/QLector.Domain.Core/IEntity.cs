namespace QLector.Domain.Core
{
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }

    public interface IEntity
    {
    }
}