using System.Security.Claims;

namespace QLector.Application.Core
{
    /// <summary>
    /// Application layer request contract
    /// </summary>
    /// <typeparam name="TRequest">Type of request DTO</typeparam>
    public interface IApplicationRequest<out TRequest>
    {
        /// <summary>
        /// Data Transfer Object
        /// </summary>
        TRequest Data { get; }

        /// <summary>
        /// Current system principal
        /// </summary>
        ClaimsPrincipal Principal { get; }
    }
}