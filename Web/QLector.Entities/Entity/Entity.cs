using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLector.Entities.Entity
{
    public abstract class Entity : Entity<int>
    {
    }

    public abstract class Entity<TKey> : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
    }
}
