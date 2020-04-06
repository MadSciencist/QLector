using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;
using QLector.Entities;
using System.Threading.Tasks;

namespace QLector.Api.Controllers
{
    /// <summary>
    /// Manages users and roles
    /// </summary>
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="loginCommand"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(Response<UserLoggedResponseModel>), 200)]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var command = CreateCommand<LoginCommand, UserLoggedResponseModel>(loginCommand);
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="registerUserCommand"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(Response<UserCreatedModel>), 201)]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            var command = CreateCommand<RegisterUserCommand, UserCreatedModel>(registerUserCommand);
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<BasicResponse>), 200)]
        [HttpDelete("{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveRole(int userId, int roleId)
        {
            var command = CreateCommand<RemoveRoleCommand, BasicResponse>(new RemoveRoleCommand { RoleId = roleId, UserId = userId });
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        [HttpPost("{userId}/roles/{roleId}")]
        [ProducesResponseType(typeof(Response<BasicResponse>), 200)]
        public async Task<IActionResult> AddRole(int userId, int roleId)
        {
            var command = CreateCommand<AddRoleCommand, BasicResponse>(new AddRoleCommand { RoleId = roleId, UserId = userId });
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
    }
}