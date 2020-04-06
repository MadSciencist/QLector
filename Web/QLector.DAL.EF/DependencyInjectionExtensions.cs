using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLector.DAL.EF.Repository;
using QLector.DAL.EF.Repository.Users;
using QLector.Domain.Abstractions;
using QLector.Domain.Abstractions.Repository.Users;

namespace QLector.DAL.EF
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkDataAccessImplementation(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>()
                .AddTransient(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>))
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(config["ConnectionString"]);

                }, ServiceLifetime.Scoped);
        }
    }
}
