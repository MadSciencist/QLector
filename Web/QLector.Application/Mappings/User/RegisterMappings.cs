using AutoMapper;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels.User;
using QLector.Security.Dto;
using UserEntity = QLector.Entities.Entity.User;

namespace QLector.Application.Mappings.User
{
    public class RegisterMappings : Profile
    {
        public RegisterMappings()
        {
            CreateMap<RegisterUserCommand, RegisterDto>();

            CreateMap<UserEntity, UserCreatedModel>();
        }
    }
}
