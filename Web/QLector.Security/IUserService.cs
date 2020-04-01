using QLector.Entities.Entity;
using QLector.Security.Dto;
using System.Threading.Tasks;

namespace QLector.Security
{
    public interface IUserService
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task<User> Register(RegisterDto registerDto); // should we return entity or rather DTO?
    }
}