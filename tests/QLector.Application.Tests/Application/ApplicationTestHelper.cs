using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QLector.Application.Core.Extensions;
using QLector.Application.Core.Infrastructure;
using QLector.DAL.EF;
using QLector.Domain.Users;
using QLector.Domain.Users.Enumerations;
using QLector.Domain.Users.Repositories;
using QLector.Security;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace QLector.Application.Tests.Application
{
    public class ApplicationTestHelper
    {
        public IServiceProvider Services { get; }
        public IConfiguration Config { get; }

        public ApplicationTestHelper(Action<IServiceCollection> services = null)
        {
            Config = new ConfigurationBuilder()
                .AddInMemoryCollection(CreateFakeConfig()).Build();
            var serviceCollection = new ServiceCollection();
            var fluentValidationConfig = new FluentValidationMvcConfiguration();
            
            var service = typeof(IValidatorFactory);
            var implementationType = fluentValidationConfig.ValidatorFactoryType;
            if ((object)implementationType == null)
                implementationType = typeof(ServiceProviderValidatorFactory);
            var serviceDescriptor = ServiceDescriptor.Transient(service, implementationType);

            serviceCollection.AddApplicationLayer(Config, new FakeHostAssemblyProvider());
            serviceCollection.AddSecurity(Config);
            serviceCollection.AddSingleton<IConfiguration>(Config);
            serviceCollection.AddEntityFrameworkDataAccessImplementation(Config, useInMemoryDb: true);
            serviceCollection.Add(serviceDescriptor);
            serviceCollection.AddLogging();
            services?.Invoke(serviceCollection);

            Services = serviceCollection.BuildServiceProvider();
        }

        public async Task AddDefaultRole()
        {
            var roleRepository = Services.GetRequiredService<IRoleRepository>();
            var regularRole = Role.Create(Roles.RegularUser);
            await roleRepository.Add(regularRole);
            await roleRepository.UnitOfWork.Commit();
        }

        public async Task AddUser(string username, string password)
        {
            var userRepository = Services.GetRequiredService<IUserRepository>();
            var hasher = Services.GetRequiredService<IPasswordHasher<Domain.Users.User>>();
            var hash = hasher.HashPassword(null, password);
            var user = Domain.Users.User.Create(username, $"{username}@test.com", hash);
            await userRepository.Add(user);
            await userRepository.UnitOfWork.Commit();
        }

        private IEnumerable<KeyValuePair<string, string>> CreateFakeConfig()
        {
            return new Dictionary<string, string>
            {
                { "Jwt:Key", "key23123asddasdasdasdasd" },
                { "Jwt:TokenLifetimeMinutes", "55" },
                { "Jwt:Issuer", "QLector" },
                { "Jwt:Audience", "*" }
            };
        }
    }

    internal class FakeHostAssemblyProvider : IHostAssemblyProvider
    {
        public Assembly GetEntryAssembly() => this.GetType().Assembly;
    }
}