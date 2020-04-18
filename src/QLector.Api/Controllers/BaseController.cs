using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLector.Api.BindModels;
using QLector.Application.Core;

namespace QLector.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected virtual PagedQueryRequest<TRequest, TResponse> CreateQuery<TRequest, TResponse>(TRequest request, PagerBindModel pagerBindModel)
        {
            var user = HttpContext.User;
            var pagedQueryRequest = new PagedQueryRequest<TRequest, TResponse>(request, user, pagerBindModel.Page, pagerBindModel.PageSize);
            return pagedQueryRequest;
        }

        protected virtual CommandRequest<TRequest, TResponse> CreateCommand<TRequest, TResponse>(TRequest request)
        {
            var user = HttpContext.User;
            return new CommandRequest<TRequest, TResponse>(request, user);
        }

        protected virtual IActionResult CreateActionResult<TResponse>(Response<TResponse> serviceResponse)
        {
            // If no messages, do not serialize the property
            if (serviceResponse.Messages.Count == 0)
                serviceResponse.Messages = null;

            return new ObjectResult(serviceResponse)
            {
                StatusCode = serviceResponse.GetResponseStatusCodeOverride() ?? StatusCodes.Status200OK
            };
        }
    }
}
