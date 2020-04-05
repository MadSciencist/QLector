using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels.User;
using System.Threading.Tasks;

namespace QLector.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var command = CreateCommand<LoginCommand, UserLoggedResponseModel>(loginCommand);
            return CreateActionResult(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            var command = CreateCommand<RegisterUserCommand, UserCreatedModel>(registerUserCommand);
            return CreateActionResult(await _mediator.Send(command));
        }
    }
}