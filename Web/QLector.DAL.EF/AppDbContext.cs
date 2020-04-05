using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLector.Entities.Entity.Users;
using System;
using System.Threading.Tasks;

namespace QLector.DAL.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleLink> UserRoleLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> configuration)
        {
            configuration.ToTable("Users");
            configuration.HasKey(x => x.Id);

            configuration.HasMany(x => x.UserRoleLinks).WithOne(x => x.User);

            var userRoleLinkNavigation = configuration.Metadata.FindNavigation(nameof(User.UserRoleLinks));
            userRoleLinkNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleLink>
    {
        public void Configure(EntityTypeBuilder<UserRoleLink> configuration)
        {
            configuration.ToTable("LinkUserRole");
            configuration.HasKey(x => new { x.UserId, x.RoleId });
            configuration.HasOne(x => x.User).WithMany(x => x.UserRoleLinks).HasForeignKey(x => x.UserId);
            configuration.HasOne(x => x.Role).WithMany(x => x.UserRoleLinks).HasForeignKey(x => x.RoleId);
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> configuration)
        {
            configuration.ToTable("Roles");
            configuration.HasKey(x => x.Id);

            configuration.HasMany(x => x.UserRoleLinks).WithOne(x => x.Role);

            var userRoleLinkNavigation = configuration.Metadata.FindNavigation(nameof(User.UserRoleLinks));
            userRoleLinkNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
