using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;
using QLector.Security;
using QLector.Security.Dto;
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

            var registerDto = Mapper.Map<RegisterDto>(request);
            var user = await _userService.Register(registerDto);

            Logger.LogInformation("User created! {@user}", user);

            result.Data = Mapper.Map<UserCreatedModel>(user);

            return result;
        }
    }
}
