using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using QLector.Domain.Abstractions;
using QLector.Domain.Abstractions.Repository.Users;
using QLector.Entities.Entity.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

// Thank you MS for those fatty interfaces
namespace QLector.Security.EFStore
{
    public partial class EntityFrameworkUserStore : IUserEmailStore<User>, IUserPasswordStore<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleLinkRepository _userRoleLinkRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<EntityFrameworkUserStore> _logger;

        public EntityFrameworkUserStore(ILogger<EntityFrameworkUserStore>  logger, IUserRepository userRepository, IUserRoleLinkRepository userRoleLinkRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userRoleLinkRepository = userRoleLinkRepository ?? throw new ArgumentNullException(nameof(userRoleLinkRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.Add(user);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(CreateAsync));
                return IdentityResult.Failed(new IdentityError { Code = nameof(CreateAsync), Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {           
            try
            {
                await _userRepository.Remove(user);
                return IdentityResult.Success;
            }
            catch(Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = nameof(DeleteAsync), Description = ex.Message });
            }
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
            return Task.FromResult(user.NormalizedUserName);
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.FindById(user.Id);
            return entity?.PasswordHash;
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.FindById(user.Id);
            return entity?.Id.ToString();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash; // TODO
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.Update(user);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(UpdateAsync));
                return IdentityResult.Failed(new IdentityError { Code = nameof(UpdateAsync), Description = ex.Message });
            }
        }

        public void Dispose()
        {
        }
    }
}
