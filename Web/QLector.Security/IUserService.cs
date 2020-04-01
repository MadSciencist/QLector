using QLector.Security.Dto;
using System.Threading.Tasks;

namespace QLector.Security
{
    public interface IUserService
    {
        Task<TokenDto> Login(LoginDto loginDto);
    }
}