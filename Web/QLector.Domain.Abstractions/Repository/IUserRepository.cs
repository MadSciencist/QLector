using QLector.Entities.Entity;
using System.Threading.Tasks;

namespace QLector.Domain.Abstractions.Repository
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindByUserName(string userName);
        Task<User> FindByEmail(string email);
    }
}
