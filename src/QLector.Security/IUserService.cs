using QLector.Domain.Core;
using QLector.Security.Dto;
using System.Threading.Tasks;

namespace QLector.Security
{
    public interface IUserService
    {
        /// <summary>
        /// Sign in user. Creates new token.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        Task<TokenDto> Login(LoginDto loginDto);

        /// <summary>
        /// Get current user profile
        /// </summary>
        /// <param name="getProfileDto"></param>
        /// <returns></returns>
        Task<UserProfileDto> GetProfile(GetProfileDto getProfileDto);

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<UserProfileDto> Register(RegisterDto registerDto, string role);

        /// <summary>
        /// Add to role
        /// </summary>
        /// <param name="removeRoleDto"></param>
        /// <returns></returns>
        Task<GenericResponse<object>> RemoveRole(AddRemoveRoleDto removeRoleDto);

        /// <summary>
        /// Remove from role
        /// </summary>
        /// <param name="removeRoleDto"></param>
        /// <returns></returns>
        Task<GenericResponse<object>> AddRole(AddRemoveRoleDto removeRoleDto);
    }
}