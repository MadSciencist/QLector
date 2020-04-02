using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLector.Entities.Entity.Users;

namespace QLector.DAL.EF
{
    public class DbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRoleLink, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleLink> UserRoleLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey(x => x.Id);

            builder.Entity<Role>().HasKey(x => x.Id);

            builder.Entity<UserRoleLink>().HasKey(x => new { x.RoleId, x.UserId });
        }
    }
}
