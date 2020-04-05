namespace QLector.Entities.Entity
{
    public abstract class Entity : Entity<int>
    {
    }

    public abstract class Entity<TKey> : IEntity
    {
        public virtual TKey Id { get; set; }
    }
}
