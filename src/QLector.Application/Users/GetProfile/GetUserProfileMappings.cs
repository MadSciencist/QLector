using AutoMapper;
using QLector.Security.Dto;

namespace QLector.Application.Users.GetProfile
{
    public class GetUserProfileMappings : Profile
    {
        public GetUserProfileMappings()
        {
            CreateMap<GetUserProfileCommand, GetProfileDto>()
                .ConstructUsing(x => new GetProfileDto(x.UserId));

            CreateMap<QLector.Security.Dto.UserProfileDto, UserProfileDto>();
        }
    }
}
