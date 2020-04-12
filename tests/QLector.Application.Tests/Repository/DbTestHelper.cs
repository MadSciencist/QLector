using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLector.DAL.EF;
using System;

namespace QLector.Application.Tests.Repository
{
    public class DbTestHelper
    {
        public IServiceProvider Services { get; }
        public IConfiguration Config { get; }

        public DbTestHelper(Action<IServiceCollection> services = null)
        {
            Config = new ConfigurationBuilder().Build();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IConfiguration>(Config);
            serviceCollection.AddEntityFrameworkDataAccessImplementation(Config, useInMemoryDb: true);
            serviceCollection.AddLogging();

            services?.Invoke(serviceCollection);

            Services = serviceCollection.BuildServiceProvider();
        }
    }
}
