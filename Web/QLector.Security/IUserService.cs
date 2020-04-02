﻿using QLector.Entities.Entity.Users;
using QLector.Entities.Enumerations.Users;
using QLector.Security.Dto;
using System.Threading.Tasks;

namespace QLector.Security
{
    public interface IUserService
    {
        Task<TokenDto> Login(LoginDto loginDto);
        Task<User> Register(RegisterDto registerDto, string role = Roles.RegularUser); // should we return entity or rather DTO?
    }
}