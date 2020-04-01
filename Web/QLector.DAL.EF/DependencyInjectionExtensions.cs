using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLector.DAL.EF.Repository;
using QLector.Domain.Abstractions;
using QLector.Domain.Abstractions.Repository;

namespace QLector.DAL.EF
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkDataAccessImplementation(this IServiceCollection services, IConfiguration config )
        {
            return services
                .AddTransient<IUnitOfWork, EntityFrameworkUnitOfWork>()
                .AddTransient(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>))
                .AddTransient<IUserRepository, UserRepository>()
                .AddDbContext<DbContext>(options =>
                {
                    options.UseSqlServer(config["ConnectionString"]);

                }, ServiceLifetime.Transient);
        }
    }
}
