using Microsoft.AspNetCore.Identity;
using QLector.Domain.Abstractions;
using QLector.Entities.Entity.Users;
using QLector.Entities.Enumerations.Users;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using QLector.Security.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLector.Security
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(IUnitOfWork unitOfWork, ITokenBuilder tokenBuilder, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            _tokenBuilder = tokenBuilder ?? throw new ArgumentNullException(nameof(tokenBuilder));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(tokenBuilder));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Login);

            if (user is null)
                throw new UserNotExistsException();

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!signInResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Incorrect login or password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var (token, validTo) = _tokenBuilder.Build(user, GetUserClaims(user, roles));

            return new TokenDto
            {
                UserId = user.Id,
                Token = token,
                ValidTo = validTo,
                IssuedAt = DateTime.UtcNow
            };
        }

        private List<Claim> GetUserClaims(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            if(roles != null && roles.Any())
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        public async Task<User> Register(RegisterDto registerDto, string role = Roles.Default)
        {
            var alreadyExistingUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (alreadyExistingUser != null)
                throw new UserAlreadyExistsException();

            User user;

            try
            {
                user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createUserResult = await _userManager.CreateAsync(user, registerDto.Password);

                if (!createUserResult.Succeeded || user.Id == 0)
                {
                    if (createUserResult.Errors.Any())
                        throw new UserCreationException(createUserResult.Errors);

                    throw new UserCreationException("Could not create user");
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(user, role);

                if (!addToRoleResult.Succeeded)
                {
                    if (createUserResult.Errors.Any())
                        throw new UserCreationException(createUserResult.Errors);

                    throw new UserCreationException("Could not create user");
                }

                await _unitOfWork.Commit();
            }
            catch(Exception)
            {
                await _unitOfWork?.Rollback();
                throw;
            }

            return user;
        }
    }
}
