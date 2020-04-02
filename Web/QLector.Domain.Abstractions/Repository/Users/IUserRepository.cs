using QLector.Entities.Entity.Users;
using System.Threading.Tasks;

namespace QLector.Domain.Abstractions.Repository.Users
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindByUserName(string userName);
        Task<User> FindByEmail(string email);
    }
}
