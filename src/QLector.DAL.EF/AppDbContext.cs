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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.UserRoleLinks).WithOne(x => x.User);

            var userRoleLinkNavigation = builder.Metadata.FindNavigation(nameof(User.UserRoleLinks));
            userRoleLinkNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleLink>
    {
        public void Configure(EntityTypeBuilder<UserRoleLink> builder)
        {
            builder.ToTable("LinkUserRole");
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.HasOne(x => x.User).WithMany(x => x.UserRoleLinks).HasForeignKey(x => x.UserId);
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
        }
    }

    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionLink>
    {
        public void Configure(EntityTypeBuilder<RolePermissionLink> builder)
        {
            builder.ToTable("LinkRolePermission");
            builder.HasKey(x => new { x.PermissionId, x.RoleId });
            builder.HasOne(x => x.Role).WithMany(x => x.RolePermissionLinks).HasForeignKey(x => x.RoleId);
        }
    }

    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);
        }
    }
}
