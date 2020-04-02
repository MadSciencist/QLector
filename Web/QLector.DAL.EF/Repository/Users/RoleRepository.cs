using QLector.Domain.Abstractions.Repository.Users;
using QLector.Entities.Entity.Users;

namespace QLector.DAL.EF.Repository.Users
{
    public class RoleRepository : EntityFrameworkRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
