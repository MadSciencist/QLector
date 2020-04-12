using System;
using Microsoft.EntityFrameworkCore;
using QLector.Domain.Users;
using QLector.Domain.Users.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository.Users
{
    public class RoleRepository : EntityFrameworkRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<Role> FindByName(string name)
            => await Context.Roles.FirstOrDefaultAsync(x => x.Name == name);

        public async Task<IEnumerable<string>> GetUserRoles(string username) 
            => await Context.UserRoleLinks
                .Include(x => x.Role)
                .Include(x => x.User)
                .Where(x => x.User.UserName == username)
                .Select(x => x.Role.Name)
                .ToListAsync();

        public async Task<IEnumerable<string>> GetUserPermissions(string username)
        {
            var user = await Context.Users
                .AsNoTracking()
                .Include(x => x.UserRoleLinks)
                .FirstOrDefaultAsync(x => x.UserName == username);

            var roleIds = user.UserRoleLinks.Select(x => x.RoleId);

            return await Context.RolePermissionLinks
                .AsNoTracking()
                .Include(x => x.Permission)
                .Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.Permission.Name)
                .ToListAsync();
        }
    }
}
