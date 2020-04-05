using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QLector.Entities.Entity.Users;

namespace QLector.Security
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJwtTokenBuilder(this IServiceCollection services)
            => services
            .AddTransient<ITokenBuilder, JwtTokenBuilder>()
            .AddTransient<IAuthorizationService, ClaimsAuthorizationService>();

        public static IServiceCollection AddUserService(this IServiceCollection services)
        {           
            return services.AddTransient<IUserService, UserService>()
                .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}
