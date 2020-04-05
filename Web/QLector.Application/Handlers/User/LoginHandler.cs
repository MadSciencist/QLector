using Microsoft.AspNetCore.Http;
using QLector.Application.Commands;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;
using QLector.Security;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Handlers.User
{
    public class LoginHandler : BaseHandler<LoginCommand, UserLoggedResponseModel>
    {
        private readonly IUserService _userService;

        public LoginHandler(IServiceProvider services, IUserService userService) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<UserLoggedResponseModel>> Handle(Request<LoginCommand, UserLoggedResponseModel> request)
        {
            var result = new Response<UserLoggedResponseModel>();

            try
            {
                var tokenDto = await _userService.Login(new LoginDto(request.Data.Login, request.Data.Password));

                result.Data = new UserLoggedResponseModel
                {
                    Token = tokenDto.Token,
                    ValidTo = tokenDto.ValidTo,
                    IssuedAt = tokenDto.IssuedAt,
                    Id = tokenDto.UserId,
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                result.AddError(ex.Message).SetStatusCodeOverride(StatusCodes.Status401Unauthorized);
            }
            catch (InvalidLoginAttemptException ex)
            {
                result.AddError(ex.Message).SetStatusCodeOverride(StatusCodes.Status400BadRequest);
            }

            return result;
        }
    }
}
