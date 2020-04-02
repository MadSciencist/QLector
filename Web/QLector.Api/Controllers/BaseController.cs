using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLector.Application.ResponseModels;

namespace QLector.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected virtual IActionResult CreateActionResult<TDto>(Response<TDto> serviceResponse)
        {
            return new ObjectResult(serviceResponse)
            {
                StatusCode = serviceResponse.ResponseStatusCodeOverride ?? StatusCodes.Status200OK
            };
        }
    }
}
