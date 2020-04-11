using System.Security.Claims;

namespace QLector.Application.Core
{
    /// <summary>
    /// Application layer request contract
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IApplicationRequest<out TRequest>
    {
        TRequest Data { get; }
        ClaimsPrincipal Principal { get; }
    }
}