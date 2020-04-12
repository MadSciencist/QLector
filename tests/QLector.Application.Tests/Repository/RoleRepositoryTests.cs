using Microsoft.Extensions.DependencyInjection;
using QLector.Domain.Users;
using QLector.Domain.Users.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace QLector.Application.Tests.Repository
{
    public class RoleRepositoryTests
    {
        private readonly IRoleRepository _roleRepository;
        private readonly Role _role;

        public RoleRepositoryTests()
        {
            var helper = new DbTestHelper();
            _roleRepository = helper.Services.GetRequiredService<IRoleRepository>();
            _role = Role.Create("approle");
        }
        

        [Fact]
        public async Task RoleRepository_Add_CanFindByName()
        {
            // Arrange
            await _roleRepository.Add(_role);
            await _roleRepository.UnitOfWork.Commit();

            // Act
            var fromRepo = await _roleRepository.FindByName(_role.Name);

            // Assert
            Assert.NotNull(fromRepo);
            Assert.Equal(_role.Name, fromRepo.Name);
        }

        [Fact]
        public async Task RoleRepository_Remove_CanRemove()
        {
            // Arrange
            const string roleName = "testRoleToRemove";
            await _roleRepository.Add(Role.Create(roleName));
            await _roleRepository.UnitOfWork.Commit();
            var toRemove = await _roleRepository.FindByName(roleName);
            
            // Act
            await _roleRepository.Remove(toRemove);
            await _roleRepository.UnitOfWork.Commit();
            var maybeRemoved = await _roleRepository.FindByName(roleName);

            //Assert
            Assert.Null(maybeRemoved);
        }
    }
}
