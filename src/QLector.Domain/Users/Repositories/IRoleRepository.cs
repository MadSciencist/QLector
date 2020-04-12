using QLector.Domain.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLector.Domain.Users.Repositories
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        /// <summary>
        /// Find by role name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Role> FindByName(string name);

        /// <summary>
        /// Find all role associated with user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetUserRoles(string username);

        /// <summary>
        /// Get permissions for username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetUserPermissions(string username);
    }
}