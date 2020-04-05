using QLector.Entities.Entity.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLector.Domain.Abstractions.Repository.Users
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        /// <summary>
        /// Find by normalized role name
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
    }
}