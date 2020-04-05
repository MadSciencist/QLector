using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLector.Application.Commands;
using QLector.Application.ResponseModels;

namespace QLector.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected virtual Request<TRequest, TResponse> CreateCommand<TRequest, TResponse>(TRequest request)
        {
            var user = HttpContext.User;
            return new Request<TRequest, TResponse>(request, user);
        }

        protected virtual IActionResult CreateActionResult<TResponse>(Response<TResponse> serviceResponse)
        {
            // If no messages, do not serialize the property
            if (serviceResponse.Messages.Count == 0)
                serviceResponse.Messages = null;

            return new ObjectResult(serviceResponse)
            {
                StatusCode = serviceResponse.ResponseStatusCodeOverride ?? StatusCodes.Status200OK
            };
        }
    }
}
