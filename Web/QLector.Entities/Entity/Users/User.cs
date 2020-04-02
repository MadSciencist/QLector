using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QLector.Entities.Entity.Users
{
    public class User : IdentityUser<int>, IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public ICollection<Document> Documents { get; set; }

        public User()
        {
            Documents = new HashSet<Document>();
        }
    }
}
