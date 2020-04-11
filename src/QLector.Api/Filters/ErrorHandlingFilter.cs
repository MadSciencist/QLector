using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QLector.Application.Core;
using QLector.Application.Core.Extensions;
using System;
using System.Linq;

namespace QLector.Api.Filters
{
    public class ErrorHandlingFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null) return;

            context.ExceptionHandled = true;

            if (context.Exception is ValidationException validationEx)
            {
                context.Result = new ObjectResult(new Response<object>
                {
                    Messages = validationEx.Errors.Select(e => e.ToResponseMessage()).ToList()
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                context.ExceptionHandled = true;
                return;
            }

            if(context.Exception is UnauthorizedAccessException unauthorizedEx)
            {
                var result = new ObjectResult(Response<object>.FromError(unauthorizedEx.Message))
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                context.Result = result;
                context.ExceptionHandled = true;
                return;
            }

            // TODO refactor, enrich problem details, hide details for not auth/admins
            var response = Response<object>.FromError(context?.Exception?.Message, new ProblemDetails
            {
                Type = context.HttpContext.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
                Title = context?.Exception?.Message,
                Detail = context?.Exception?.ToString(),
            });

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
