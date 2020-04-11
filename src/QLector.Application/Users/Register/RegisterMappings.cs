using AutoMapper;
using QLector.Security.Dto;

namespace QLector.Application.Users.Register
{
    public class RegisterMappings : Profile
    {
        public RegisterMappings()
        {
            CreateMap<RegisterUserCommand, RegisterDto>();

            CreateMap<UserProfileDto, UserCreatedDto>();
        }
    }
}
