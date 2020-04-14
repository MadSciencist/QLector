using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLector.DAL.EF.Repository;
using QLector.DAL.EF.Repository.Users;
using QLector.Domain.Core;
using QLector.Domain.Users.Repositories;

namespace QLector.DAL.EF
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkDataAccessImplementation(this IServiceCollection services, IConfiguration config, bool useInMemoryDb = false)
        {
            var inMemDb = config.GetValue<bool>("InMemoryDatabase");

            return services
                .AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>()
                .AddTransient(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>))
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IDbInitializer, EntityFrameworkDbInitializer>()
                .AddDbContext<AppDbContext>(options =>
                {
                    // refactor
                    if (inMemDb || useInMemoryDb)
                    {
                        options.UseInMemoryDatabase("test");
                    }
                    else
                    {
                        options.UseSqlServer(config["ConnectionString"]);
                    }

                }, ServiceLifetime.Scoped);
        }
    }
}
