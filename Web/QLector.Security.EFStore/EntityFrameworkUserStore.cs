using Microsoft.AspNetCore.Identity;
using QLector.Domain.Abstractions;
using QLector.Domain.Abstractions.Repository;
using QLector.Entities.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Security.EFStore
{
    public class EntityFrameworkUserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IUserRepository _userRepository;

        public EntityFrameworkUserStore(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitofWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var id = int.Parse(userId);
            return await _userRepository.FindById(id);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userRepository.FindByUserName(normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.FindById(user.Id);
            return entity?.PasswordHash; // TODO hashing/salting
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.FindById(user.Id);
            return entity?.Id.ToString();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        // DI container will control lifetime
        public void Dispose()
        {
        }
    }
}
