using QLector.Domain.Abstractions.Repository.Users;
using QLector.Entities.Entity.Users;

namespace QLector.DAL.EF.Repository.Users
{
    public class UserRoleLinkRepository : EntityFrameworkRepository<UserRoleLink, int>, IUserRoleLinkRepository
    {
        public UserRoleLinkRepository(AppDbContext context) : base(context)
        {
        }
    }
}
