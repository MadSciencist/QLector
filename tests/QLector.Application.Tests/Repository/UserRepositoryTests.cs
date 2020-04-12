using Microsoft.Extensions.DependencyInjection;
using QLector.Domain.Users;
using QLector.Domain.Users.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace QLector.Application.Tests.Repository
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userRepository;
        private readonly User _user;

        public UserRepositoryTests()
        {
            var helper = new DbTestHelper();
            _userRepository = helper.Services.GetRequiredService<IUserRepository>();
            _user = User.Create("matty", "matty@matty.com", "s3cure");
        }

        [Fact]
        public async Task UserRepository_Add_CanAddUser()
        {
            // Arrange
            await _userRepository.Add(_user);
            await _userRepository.UnitOfWork.Commit();

            // Act
            var fromRepo = await _userRepository.FindByUserName(_user.UserName);

            // Assert
            Assert.Equal(_user.UserName, fromRepo.UserName);
            Assert.Equal(_user.Email, fromRepo.Email);
            Assert.Equal(_user.Password, fromRepo.Password);
        }

        [Fact]
        public async Task UserRepository_FindUser_CanFindByUserName()
        {
            // Arrange
            await _userRepository.Add(_user);
            await _userRepository.UnitOfWork.Commit();

            // Act
            var user = await _userRepository.FindByUserName(_user.UserName);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(_user.Email, user.Email);
            Assert.Equal(_user.UserName, user.UserName);
        }

        [Fact]
        public async Task UserRepository_FindUser_CanFindByEmail()
        {
            // Arrange
            await _userRepository.Add(_user);
            await _userRepository.UnitOfWork.Commit();

            // Act
            var user = await _userRepository.FindByEmail(_user.Email);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(_user.Email, user.Email);
            Assert.Equal(_user.UserName, user.UserName);
        }

        [Fact]
        public async Task UserRepository_Remove_CanRemoveUser()
        {
            // Arrange
            await _userRepository.Add(_user);
            await _userRepository.UnitOfWork.Commit();

            // Act
            var user = await _userRepository.FindByEmail(_user.Email);
            await _userRepository.Remove(user);
            await _userRepository.UnitOfWork.Commit();
            var maybeRemoved = await _userRepository.FindByEmail(_user.Email);

            // Assert
            Assert.Null(maybeRemoved);
        }
    }
}
