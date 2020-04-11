using Microsoft.AspNetCore.Identity;
using QLector.Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Security.EFStore
{
    public partial class EntityFrameworkUserStore : IUserRoleStore<User>
    {
        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Find(x => x.NormalizedName == roleName);

            if (role is null)
                throw new ArgumentNullException($"Role {roleName} doesn't exists");

            user.AddToRole(role);

            //await _userRoleLinkRepository.Add(new UserRoleLink { RoleId = role.Id, UserId = user.Id });
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //var role = await _roleRepository.Find(x => x.NormalizedName == roleName);

            //if (role is null)
            //    throw new ArgumentNullException($"Role {roleName} doesn't exists");

            //await _userRoleLinkRepository.Remove(new UserRoleLink { UserId = user.Id, RoleId = role.Id });
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //var roleIds = (await _userRoleLinkRepository.GetManyFiltered(x => x.UserId == user.Id))
            //    .Select(x => x.RoleId);

            //var roles = await _roleRepository.GetManyFiltered(x => roleIds.Contains(x.Id));

            //return !roles.Any() ? new List<string>() : roles.Select(x => x.Name).ToList();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var roleIds = (await _userRoleLinkRepository.GetManyFiltered(x => x.UserId == user.Id)).Select(x => x.RoleId);

            if (!roleIds.Any()) return false;

            var roles = await _roleRepository.GetManyFiltered(x => roleIds.Contains(x.Id));

            return !roles.Any() ? false : roles.Any(x => x.NormalizedName == roleName);
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}