using QLector.Domain.Core;
using System.Threading.Tasks;

namespace QLector.Domain.Users.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindByUserName(string userName);
        Task<User> FindByEmail(string email);
    }
}