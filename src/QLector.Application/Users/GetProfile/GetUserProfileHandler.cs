using QLector.Application.Core;
using QLector.Security;
using QLector.Security.Dto;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Users.GetProfile
{
    public class GetUserProfileHandler : BaseHandler<GetUserProfileCommand, UserProfileDto>
    {
        private readonly IUserService _userService;

        public GetUserProfileHandler(IUserService userService, IServiceProvider services) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<UserProfileDto>> Handle(Request<GetUserProfileCommand, UserProfileDto> request)
        {
            var getProfileParam = Mapper.Map<GetProfileDto>(request.Data);
            var user = await _userService.GetProfile(getProfileParam);
            return new Response<UserProfileDto> {Data = Mapper.Map<UserProfileDto>(user)};
        }
    }
}