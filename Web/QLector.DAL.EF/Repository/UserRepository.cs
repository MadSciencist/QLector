using Microsoft.EntityFrameworkCore;
using QLector.Domain.Abstractions.Repository;
using QLector.Entities.Entity;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository
{
    public class UserRepository : EntityFrameworkRepository<User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<User> FindByUserName(string userName)
        {
            return await Context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NormalizedUserName == userName);
        }
    }
}
