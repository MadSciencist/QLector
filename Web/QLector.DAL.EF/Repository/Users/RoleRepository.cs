using Microsoft.EntityFrameworkCore;
using QLector.Domain.Abstractions.Repository.Users;
using QLector.Entities.Entity.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository.Users
{
    public class RoleRepository : EntityFrameworkRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Role> FindByName(string name)
            => await Context.Roles.FirstOrDefaultAsync(x => x.NormalizedName == name);

        public async Task<IEnumerable<string>> GetUserRoles(string username) 
            => await Context.UserRoleLinks
                .Include(x => x.Role)
                .Include(x => x.User)
                .Where(x => x.User.UserName == username)
                .Select(x => x.Role.Name)
                .ToListAsync();
    }
}
