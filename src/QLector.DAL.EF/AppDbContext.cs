using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLector.Domain.Users;

namespace QLector.DAL.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleLink> UserRoleLinks { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissionLink> RolePermissionLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new RolePermissionConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
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
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> configuration)
        {
            configuration.ToTable("Roles");
            configuration.HasKey(x => x.Id);
        }
    }

    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionLink>
    {
        public void Configure(EntityTypeBuilder<RolePermissionLink> configuration)
        {
            configuration.ToTable("LinkRolePermission");
            configuration.HasKey(x => new { x.PermissionId, x.RoleId });
            configuration.HasOne(x => x.Role).WithMany(x => x.RolePermissionLinks).HasForeignKey(x => x.RoleId);
        }
    }

    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> configuration)
        {
            configuration.ToTable("Permissions");
            configuration.HasKey(x => x.Id);
        }
    }
}
