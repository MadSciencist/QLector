using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLector.Api.BindModels;
using QLector.Application.Core;
using QLector.Application.Users.AddRole;
using QLector.Application.Users.GetProfile;
using QLector.Application.Users.GetProfiles;
using QLector.Application.Users.Login;
using QLector.Application.Users.Register;
using QLector.Application.Users.RemoveRole;
using System.Collections.Generic;
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
        /// Get user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId:int}")]
        [ProducesResponseType(typeof(Response<UserProfileDto>), 200)]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var commandParam = new GetUserProfileCommand { UserId = userId };
            var command = CreateCommand<GetUserProfileCommand, UserProfileDto>(commandParam);
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<UserProfileDto>>), 200)]
        public async Task<IActionResult> GetProfiles([FromQuery]PagerBindModel pagerBindModel)
        {
            var query = CreateQuery<GetProfilesQuery, UserProfileDto> (new GetProfilesQuery(), pagerBindModel);
            var result = await _mediator.Send(query);
            return CreateActionResult(result);
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="loginCommand"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(Response<UserLoggedDto>), 200)]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var command = CreateCommand<LoginCommand, UserLoggedDto>(loginCommand);
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
        [ProducesResponseType(typeof(Response<UserCreatedDto>), 201)]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            var command = CreateCommand<RegisterUserCommand, UserCreatedDto>(registerUserCommand);
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<IsSuccessResponse>), 200)]
        [HttpDelete("{userId}/roles/{roleId}")]
        public async Task<IActionResult> RemoveRole(int userId, int roleId)
        {
            var command = CreateCommand<RemoveRoleCommand, IsSuccessResponse>(new RemoveRoleCommand { RoleId = roleId, UserId = userId });
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        [HttpPost("{userId}/roles/{roleId}")]
        [ProducesResponseType(typeof(Response<IsSuccessResponse>), 200)]
        public async Task<IActionResult> AddRole(int userId, int roleId)
        {
            var command = CreateCommand<AddRoleCommand, IsSuccessResponse>(new AddRoleCommand { RoleId = roleId, UserId = userId });
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
    }
}