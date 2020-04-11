using Microsoft.AspNetCore.Http;
using QLector.Application.Core;
using QLector.Domain.Core;
using QLector.Security;
using QLector.Security.Dto;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Users.AddRole
{
    public class AddRoleHandler : BaseHandler<AddRoleCommand, IsSuccessResponse>
    {
        private readonly IUserService _userService;

        public AddRoleHandler(IServiceProvider services, IUserService userService) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<IsSuccessResponse>> Handle(Request<AddRoleCommand, IsSuccessResponse> request)
        {
            var result = new Response<IsSuccessResponse>();

            try
            {
                var serviceResponse = await _userService.AddRole(new AddRemoveRoleDto(request.Data.UserId, request.Data.RoleId));
                result.AddMessages(serviceResponse.Messages);
                result.Data = new IsSuccessResponse(serviceResponse.IsSuccess);
            }
            catch (DomainException ex)
            {
                result.AddError(ex.Message).SetStatusCodeOverride(StatusCodes.Status400BadRequest);
            }

            return result;
        }
    }
}
