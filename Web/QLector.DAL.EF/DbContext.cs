using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLector.Entities.Entity;

namespace QLector.DAL.EF
{
    public class DbContext : IdentityDbContext<User, Role, int>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
    }
}
