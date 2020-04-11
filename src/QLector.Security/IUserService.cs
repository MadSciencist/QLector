using QLector.Domain.Core;
using QLector.Domain.Enumerations.Users;
using QLector.Security.Dto;
using System.Threading.Tasks;

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