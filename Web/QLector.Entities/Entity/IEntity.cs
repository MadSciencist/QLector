namespace QLector.Entities.Entity
{
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }

    public interface IEntity
    {
    }
}
