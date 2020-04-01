using MediatR;
using QLector.Application.Commands.Login;
using QLector.Application.ResponseModels;
using QLector.Domain.Infrastructure.Exceptions;
using QLector.Security;
using QLector.Security.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Handlers
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
                    IssuedAt = tokenDto.IssuedAt
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                // handle
            }
            catch (UserNotExistsException ex)
            {
                Console.WriteLine("not exissts");
            }
            catch (NotFoundException ex)
            {
                // handle
            }

            return result;
        }
    }
}
