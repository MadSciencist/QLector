using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QLector.Domain.Core;
using QLector.Domain.Users;

namespace QLector.Security
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddTransient<ITokenBuilder, JwtTokenBuilder>()
                .AddTransient<IAuthorizationService, ClaimsAuthorizationService>()
                .AddTransient<IUserService, UserService>()
                .AddScoped<IPasswordHasher<User>, PasswordHasher<User>>()
                .Configure<TokenOptionsSection>(config.GetSection("Jwt"))
                .AddSingleton<IValidatable>(serviceProvider =>
                    serviceProvider.GetRequiredService<IOptions<TokenOptionsSection>>().Value);
        }
    }
}
