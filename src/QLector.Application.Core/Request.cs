using MediatR;
using System.Security.Claims;

namespace QLector.Application.Core
{
    /// <summary>
    /// Application layer request container
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponseDto"></typeparam>
    public class CommandRequest<TRequest, TResponseDto> : IRequest<Response<TResponseDto>>, IApplicationRequest<TRequest>
    {
        public TRequest Data { get; }
        public ClaimsPrincipal Principal { get; }

        public CommandRequest(TRequest data, ClaimsPrincipal claimsPrincipal)
        {
            Data = data;
            Principal = claimsPrincipal;
        }
    }
}   
