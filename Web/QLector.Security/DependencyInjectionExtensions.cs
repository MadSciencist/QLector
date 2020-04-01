using Microsoft.Extensions.DependencyInjection;

namespace QLector.Security
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJwtTokenBuilder(this IServiceCollection services) 
            => services.AddTransient<ITokenBuilder, JwtTokenBuilder>();

        public static IServiceCollection AddUserService(this IServiceCollection services)
            => services.AddTransient<IUserService, UserService>();
    }
}
