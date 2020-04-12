using QLector.Domain.Core;
using QLector.Security.Dto;
using System.Threading.Tasks;
using QLector.Domain.Users.Enumerations;

namespace QLector.Security
{
    public interface IUserService
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task<UserProfileDto> GetProfile(GetProfileDto getProfileDto);
        Task<UserProfileDto> Register(RegisterDto registerDto, string role = Roles.RegularUser);
        Task<GenericResponse<object>> RemoveRole(AddRemoveRoleDto removeRoleDto);
        Task<GenericResponse<object>> AddRole(AddRemoveRoleDto removeRoleDto);
    }
}