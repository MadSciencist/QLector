using Microsoft.EntityFrameworkCore;
using QLector.Domain.Abstractions.Repository.Users;
using QLector.Entities.Entity.Users;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository.Users
{
    public class UserRepository : EntityFrameworkRepository<User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<User> FindByEmail(string email)
        {
            return await Context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NormalizedEmail == email);
        }

        public async Task<User> FindByUserName(string userName)
        {
            return await Context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NormalizedUserName == userName);
        }
    }
}
