using MediatR;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;
using QLector.Security;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Handlers.User
{
    public class LoginHandler : IRequestHandler<LoginCommand, Response<UserLoggedResponseModel>>
    {
        private readonly IUserService _userService;

        public LoginHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Response<UserLoggedResponseModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<UserLoggedResponseModel>();

            try
            {
                var tokenDto = await _userService.Login(new LoginDto(request.Login, request.Password));

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
                // handle
                throw;
            }
            catch (UserNotExistsException ex)
            {
                Console.WriteLine("not exissts");
                throw;
            }

            return result;
        }
    }
}
