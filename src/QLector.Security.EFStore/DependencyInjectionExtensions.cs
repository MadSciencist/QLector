using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QLector.Entities.Entity.Users;

namespace QLector.Security.EFStore
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddRoleManager<RoleManager<Role>>()
                .AddDefaultTokenProviders()
                .AddRoles<Role>();

            // Custom stores
            services.AddTransient<IUserStore<User>, EntityFrameworkUserStore>();
            services.AddTransient<IRoleStore<Role>, EntityFrameworkRoleStore>();

            return services;
        }
    }
}
