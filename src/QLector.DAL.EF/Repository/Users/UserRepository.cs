using System;
using Microsoft.EntityFrameworkCore;
using QLector.Domain.Users;
using QLector.Domain.Users.Repositories;
using System.Threading.Tasks;

namespace QLector.DAL.EF.Repository.Users
{
    public class UserRepository : EntityFrameworkRepository<User, int>, IUserRepository
    {
        public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<User> FindByEmail(string email)
            => await Context.Users
                .Include(x => x.UserRoleLinks)
                .FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> FindByUserName(string userName)
            => await Context.Users
                .Include(x => x.UserRoleLinks)
                .FirstOrDefaultAsync(x => x.UserName == userName);

        public override async Task<User> FindById(int id)
            => await Context.Users
                .Include(x => x.UserRoleLinks)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
