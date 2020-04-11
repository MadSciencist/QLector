using Microsoft.AspNetCore.Http;
using QLector.Application.Core;
using QLector.Security;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Users.Login
{
    public class LoginHandler : BaseHandler<LoginCommand, UserLoggedDto>
    {
        private readonly IUserService _userService;

        public LoginHandler(IServiceProvider services, IUserService userService) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<UserLoggedDto>> Handle(Request<LoginCommand, UserLoggedDto> request)
        {
            var result = new Response<UserLoggedDto>();

            try
            {
                var tokenDto = await _userService.Login(new LoginDto(request.Data.Login, request.Data.Password));

                result.Data = new UserLoggedDto
                {
                    Token = tokenDto.Token,
                    ValidTo = tokenDto.ValidTo,
                    IssuedAt = tokenDto.IssuedAt,
                    UserId = tokenDto.UserId,
                };
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is InvalidLoginAttemptException)
            {
                result.AddError(ex.Message).SetStatusCodeOverride(StatusCodes.Status401Unauthorized);
            }

            return result;
        }
    }
}
