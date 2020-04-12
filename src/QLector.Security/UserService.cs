using Microsoft.AspNetCore.Identity;
using QLector.Domain.Core;
using QLector.Domain.Users;
using QLector.Domain.Users.Repositories;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using QLector.Domain.Users.Enumerations;

namespace QLector.Security
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUnitOfWork unitOfWork, ITokenBuilder tokenBuilder, IPasswordHasher<User> passwordHasher, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            _tokenBuilder = tokenBuilder ?? throw new ArgumentNullException(nameof(tokenBuilder));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentException(nameof(roleRepository));
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userRepository.FindByUserName(loginDto.Login);

                if (user is null)
                    throw new InvalidLoginAttemptException("User with given credentials doesn't exists");

                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                    throw new UnauthorizedAccessException("Invalid credentials");

                var roles = await _roleRepository.GetUserRoles(loginDto.Login);
                var permissions = await _roleRepository.GetUserPermissions(loginDto.Login);

                var (token, validTo) = _tokenBuilder.Build(user, GetUserClaims(user, roles, permissions));

                user.SignIn();
                await _userRepository.Update(user);
                await _unitOfWork.Commit();

                return new TokenDto
                {
                    UserId = user.Id,
                    Token = token,
                    ValidTo = validTo,
                    IssuedAt = DateTime.UtcNow
                };
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<UserProfileDto> GetProfile(GetProfileDto getProfileDto)
        {
            var user = await _userRepository.FindById(getProfileDto.Id);

            if (user is null)
                throw new InvalidLoginAttemptException("User with given credentials doesn't exists");

            return new UserProfileDto(user.Id, user.UserName, user.Email, user.Created, user.Modified, user.LastLogged);
        }

        private IEnumerable<Claim> GetUserClaims(User user, IEnumerable<string> roles, IEnumerable<string> permissions)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roleList = roles.ToList();
            if (roleList.Any())
                claims.AddRange(roleList.Select(role => new Claim(ClaimTypes.Role, role)));

            var permissionList = permissions.ToList();
            if (permissionList.Any())
                claims.AddRange(permissionList.Select(p => new Claim(PermissionClaims.PermissionClaimNamespace, p)));

            return claims;
        }

        public async Task<UserProfileDto> Register(RegisterDto registerDto, string role = Roles.Default)
        {
            var alreadyExistingUser = await _userRepository.FindByUserName(registerDto.UserName.ToUpperInvariant());

            if (alreadyExistingUser != null)
                throw new UserCreationException("User with given username already exists");

            if (await _userRepository.FindByEmail(registerDto.Email) != null)
                throw new UserCreationException("User with given email already exists");

            try
            {
                var passwordHash = _passwordHasher.HashPassword(null, registerDto.Password);
                var user = User.Create(registerDto.UserName, registerDto.Email, passwordHash);

                var roleEntity = await _roleRepository.FindByName(role);
                user.AddToRole(roleEntity);
                await _userRepository.Add(user);
                await _unitOfWork.Commit();

                return new UserProfileDto(user.Id, user.UserName, user.Email, user.Created);
            }
            catch (Exception)
            {
                await _unitOfWork?.Rollback();
                throw;
            }
        }

        public async Task<GenericResponse<object>> RemoveRole(AddRemoveRoleDto removeRoleDto)
        {
            try
            {
                var user = await _userRepository.FindById(removeRoleDto.UserId);

                if (user is null)
                    return new GenericResponse<object>(new object(), false).AddErrorMessage("User doesn't exists");

                var role = await _roleRepository.FindById(removeRoleDto.RoleId);
                
                var removed = user.RemoveRole(role);

                await _unitOfWork.Commit();

                var response = new GenericResponse<object>(new object(), removed);
                if (removed)
                    response.AddInfoMessage("Successfully removed");
                else response.AddErrorMessage("Cannot remove - check if role exists");

                return response;
            }
            catch (Exception)
            {
                await _unitOfWork?.Rollback();
                throw;
            }
        }

        public async Task<GenericResponse<object>> AddRole(AddRemoveRoleDto addRoleDto)
        {
            try
            {
                var user = await _userRepository.FindById(addRoleDto.UserId);

                if (user is null)
                    return new GenericResponse<object>(new object(), false).AddErrorMessage("User doesn't exists");

                var role = await _roleRepository.FindById(addRoleDto.RoleId);

                user.AddToRole(role);

                await _unitOfWork.Commit();
                return new GenericResponse<object>(new object()).AddInfoMessage("Successfully added");
            }
            catch (Exception)
            {
                await _unitOfWork?.Rollback();
                throw;
            }
        }
    }
}
