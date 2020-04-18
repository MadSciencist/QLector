using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QLector.Application.Core;
using QLector.Domain.Users.Enumerations;
using QLector.Security;
using QLector.Security.Dto;
using QLector.Security.Exceptions;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Users.Register
{
    public class RegisterUserHandler : BaseCommandHandler<RegisterUserCommand, UserCreatedDto>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService, IServiceProvider services) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<UserCreatedDto>> Handle(CommandRequest<RegisterUserCommand, UserCreatedDto> request)
        {
            var result = new Response<UserCreatedDto>();

            Logger.LogInformation("Creating new user...");

            try
            {
                var registerDto = Mapper.Map<RegisterDto>(request.Data);
                var user = await _userService.Register(registerDto, Roles.Default);

                Logger.LogInformation("User created! {@user}", user);

                result.Data = Mapper.Map<UserCreatedDto>(user);
                result.SetStatusCodeOverride(StatusCodes.Status201Created)
                    .AddInformation("User created");
            }
            catch (UserCreationException ex)
            {
                result.AddError(ex.Message);
                result.SetStatusCodeOverride(StatusCodes.Status400BadRequest);
            }

            return result;
        }
    }
}