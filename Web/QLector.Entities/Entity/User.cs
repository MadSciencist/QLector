using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QLector.Entities.Entity
{
    public class User : IdentityUser<int>, IEntity
    {
        public ICollection<Document> Documents { get; set; }

        public User()
        {
            Documents = new HashSet<Document>();
        }
    }
}
