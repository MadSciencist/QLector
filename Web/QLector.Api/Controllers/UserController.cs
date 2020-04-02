using MediatR;
using Microsoft.AspNetCore.Mvc;
using QLector.Application.Commands.User;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            return CreateActionResult(await _mediator.Send(loginCommand));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            return CreateActionResult(await _mediator.Send(registerUserCommand));
        }
    }
}