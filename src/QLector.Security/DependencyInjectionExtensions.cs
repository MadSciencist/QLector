using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QLector.Domain.Users;

namespace QLector.Security
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
            => services
            .AddTransient<ITokenBuilder, JwtTokenBuilder>()
            .AddTransient<IAuthorizationService, ClaimsAuthorizationService>()
            .AddTransient<IUserService, UserService>()
            .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}
