using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QLector.Domain.Users;
using QLector.Domain.Users.Repositories;
using System;
using System.Threading.Tasks;
using QLector.Domain.Users.Enumerations;

namespace QLector.DAL.EF
{
    public class EntityFrameworkDbInitializer : IDbInitializer
    {
        private readonly ILogger<EntityFrameworkDbInitializer> _logger;
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkDbInitializer(IServiceProvider serviceProvider, ILogger<EntityFrameworkDbInitializer> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Initialize()
        {
            _logger.LogInformation("Initializing database...");

            using (var scope = _serviceProvider.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (context.Database.EnsureCreated())
                    _logger.LogInformation("Database created!");
            }

            return Task.CompletedTask;
        }

        public async Task Seed()
        {
            // refactor, consider using .sql files for seeding
            _logger.LogInformation("Seeding database...");

            using var scope = _serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await AddPermissions(context);
            await context.SaveChangesAsync();

            await AddRoles(scope.ServiceProvider, context);
            await context.SaveChangesAsync();

            await AddUsers(scope.ServiceProvider);
            await context.SaveChangesAsync();

            _logger.LogInformation("Done!");
        }

        public async Task AddPermissions(AppDbContext context)
        {
            if(!await context.Permissions.AnyAsync())
            {
                _logger.LogInformation($"Seeding Users.Admin permission...");
                context.Permissions.Add(Permission.Create("Users.Admin", "Can manade users"));
                context.Permissions.Add(Permission.Create("Users.ManageUsers", "Can manade users"));
                context.Permissions.Add(Permission.Create("Users.ManageRoles", "Can manade users"));
                context.Permissions.Add(Permission.Create("Users.ManagePermissions", "Can manade users"));
            }
        }

        public async Task AddRoles(IServiceProvider serviceProvider, AppDbContext context)
        {
            var roleRepo = serviceProvider.GetRequiredService<IRoleRepository>();

            if(await roleRepo.FindByName(Roles.RegularUser) is null)
            {
                _logger.LogInformation($"Seeding {Roles.RegularUser} role...");
                var regularRole = Role.Create(Roles.RegularUser);
                await roleRepo.Add(regularRole);
            }
            if (await roleRepo.FindByName(Roles.AdminUser) is null)
            {
                _logger.LogInformation($"Seeding {Roles.AdminUser} role...");

                var permissionAdmin = await context.Permissions.FirstOrDefaultAsync(x => x.Name == "Users.Admin");
                var permissionManageUsers = await context.Permissions.FirstOrDefaultAsync(x => x.Name == "Users.ManageUsers");
                var permissionManageRoles = await context.Permissions.FirstOrDefaultAsync(x => x.Name == "Users.ManageRoles");
                var permissionManagePermissions = await context.Permissions.FirstOrDefaultAsync(x => x.Name == "Users.ManagePermissions");

                var regularRole = Role.Create(Roles.AdminUser);

                regularRole.AddPermission(permissionAdmin);
                regularRole.AddPermission(permissionManageUsers);
                regularRole.AddPermission(permissionManageRoles);
                regularRole.AddPermission(permissionManagePermissions);

                await roleRepo.Add(regularRole);
            }
        }

        public async Task AddUsers(IServiceProvider serviceProvider)
        {
            var roleRepo = serviceProvider.GetRequiredService<IRoleRepository>();
            var userRepo = serviceProvider.GetRequiredService<IUserRepository>();

            if (await userRepo.FindByUserName("matty") is null)
            {
                _logger.LogInformation("Seeding admin user...");
                var regularRole = await roleRepo.FindByName(Roles.RegularUser);
                var adminRole = await roleRepo.FindByName(Roles.AdminUser);
                var user = User.Create("matty", "matty@matty.com", "AQAAAAEAACcQAAAAENfHTa6w6NIgL0mURPDa0OcnAjyDO41sFkfa5p8dIRJQhi1KuiDeyChpbtijuNFACQ==");
                user.AddToRole(regularRole);
                user.AddToRole(adminRole);
                await userRepo.Add(user);
            }
        }
    }
}
