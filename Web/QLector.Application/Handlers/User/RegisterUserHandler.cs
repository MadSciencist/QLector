using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;
using QLector.Security;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using QLector.Security.Exceptions.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Handlers.User
{
    public class RegisterUserHandler : BaseHandler, IRequestHandler<RegisterUserCommand, Response<UserCreatedModel>>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService, IMapper mapper, ILogger<RegisterUserHandler> logger) : base(mapper, logger)
        {
            _userService = userService;
        }

        public async Task<Response<UserCreatedModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<UserCreatedModel>();

            Logger.LogInformation("Creating new user...");

            try
            {
                var registerDto = Mapper.Map<RegisterDto>(request);
                var user = await _userService.Register(registerDto);

                Logger.LogInformation("User created! {@user}", user);

                result.Data = Mapper.Map<UserCreatedModel>(user);
            }
            catch (UserAlreadyExistsException)
            {
                result.AddError("Given username already exists");
                result.ResponseStatusCodeOverride = StatusCodes.Status400BadRequest;
            }
            catch (UserCreationException ex)
            {
                if (ex?.Errors != null && ex.Errors.Any())
                    result.Messages = ex.Errors.Select(e => Message.Error(e?.Description)).ToList();
                else
                {
                    result.AddError(ex.Message);
                    throw;
                }

                result.ResponseStatusCodeOverride = StatusCodes.Status400BadRequest;
            }

            return result;
        }
    }
}
