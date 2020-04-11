using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QLector.DAL.EF
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"../QLector.API/"))
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("93be7fea-e185-48d2-b1ad-74192f9f9c01")
                .Build();

            var connectionString = configuration["ConnectionString"];

            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            return new AppDbContext(builder.Options);
        }
    }
}
