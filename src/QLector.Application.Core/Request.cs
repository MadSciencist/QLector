using MediatR;
using System.Security.Claims;

namespace QLector.Application.Core
{
    /// <summary>
    /// Application layer request container
    /// </summary>
    /// <typeparam name="TRequest">Type of request DTO</typeparam>
    /// <typeparam name="TResponseDto">Type of response DTO</typeparam>
    public class CommandRequest<TRequest, TResponseDto> : IRequest<Response<TResponseDto>>, IApplicationRequest<TRequest>
    {
        /// <summary>
        /// Data Transfer Object
        /// </summary>
        public TRequest Data { get; }

        /// <summary>
        /// Current system principal
        /// </summary>
        public ClaimsPrincipal Principal { get; }

        public CommandRequest(TRequest data, ClaimsPrincipal claimsPrincipal)
        {
            Data = data;
            Principal = claimsPrincipal;
        }
    }
}   
