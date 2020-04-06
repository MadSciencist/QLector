﻿using Microsoft.AspNetCore.Http;
using QLector.Application.Commands;
using QLector.Application.Commands.User;
using QLector.Application.ResponseModels;
using QLector.Entities;
using QLector.Security;
using QLector.Security.Dto;
using System;
using System.Threading.Tasks;

namespace QLector.Application.Handlers.User
{
    public class AddRoleHandler : BaseHandler<AddRoleCommand, BasicResponse>
    {
        private readonly IUserService _userService;

        public AddRoleHandler(IServiceProvider services, IUserService userService) : base(services)
        {
            _userService = userService;
        }

        protected override async Task<Response<BasicResponse>> Handle(Request<AddRoleCommand, BasicResponse> request)
        {
            var result = new Response<BasicResponse>();

            try
            {
                var serviceResponse = await _userService.AddRole(new AddRemoveRoleDto(request.Data.UserId, request.Data.RoleId));
                result.Data = serviceResponse;

                if (!string.IsNullOrWhiteSpace(serviceResponse.Message))
                    result.Messages.Add(Message.Info(serviceResponse.Message));
            }
            catch (DomainException ex)
            {
                result.AddError(ex.Message).SetStatusCodeOverride(StatusCodes.Status400BadRequest);
            }

            return result;
        }
    }
}
