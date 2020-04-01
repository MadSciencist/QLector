using Microsoft.AspNetCore.Identity;
using QLector.Domain.Infrastructure.Exceptions;
using QLector.Entities.Entity;
using QLector.Security.Dto;
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
            {
                throw new UserNotExistsException();
            }

            await _signInManager.SignOutAsync(); // terminate existing session

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (!signInResult.Succeeded)
            {
                throw new UnauthorizedAccessException("Incorrect login or password");
            }

            var (token, validTo) = _tokenBuilder.Build(user, new List<Claim>());

            return new TokenDto
            {
                Token = token,
                ValidTo = validTo,
                IssuedAt = DateTime.UtcNow
            };
        }
    }
}
