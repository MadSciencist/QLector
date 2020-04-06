using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
    [RequirePermission("name")]
    public class RegisterUserHandler : BaseHandler<RegisterUserCommand, UserCreatedModel>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService, IServiceProvider services) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<UserCreatedModel>> Handle(Request<RegisterUserCommand, UserCreatedModel> request)
        {
            var result = new Response<UserCreatedModel>();

            Logger.LogInformation("Creating new user...");

            try
            {
                var registerDto = Mapper.Map<RegisterDto>(request.Data);
                var user = await _userService.Register(registerDto);

                Logger.LogInformation("User created! {@user}", user);

                result.Data = Mapper.Map<UserCreatedModel>(user);
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