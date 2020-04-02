using AutoMapper;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels.User;
using QLector.Entities.Entity.Users;
using QLector.Security.Dto;

namespace QLector.Application.Mappings.Users
{
    public class RegisterMappings : Profile
    {
        public RegisterMappings()
        {
            CreateMap<RegisterUserCommand, RegisterDto>();

            CreateMap<User, UserCreatedModel>();
        }
    }
}
