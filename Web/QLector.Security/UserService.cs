using Microsoft.AspNetCore.Identity;
using QLector.Entities.Entity;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using QLector.Security.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLector.Security
{
    public class UserService : IUserService
    {
        private readonly ITokenBuilder _tokenBuilder;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(ITokenBuilder tokenBuilder, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _tokenBuilder = tokenBuilder;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Login);

            if (user is null)
                throw new UserNotExistsException();

            await _signInManager.SignOutAsync(); // terminate existing session

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (!signInResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Incorrect login or password");
            }

            var (token, validTo) = _tokenBuilder.Build(user, new List<Claim>());

            return new TokenDto
            {
                UserId = user.Id,
                Token = token,
                ValidTo = validTo,
                IssuedAt = DateTime.UtcNow
            };
        }

        public async Task<User> Register(RegisterDto registerDto)
        {
            var alreadyExistingUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (alreadyExistingUser != null)
                throw new UserAlreadyExistsException();

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var createUserResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (!createUserResult.Succeeded)
                throw new UserCreationException(createUserResult.Errors);

            return user;
        }
    }
}
