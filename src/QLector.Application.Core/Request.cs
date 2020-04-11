using System.Security.Claims;
using MediatR;

namespace QLector.Application.Core
{
    /// <summary>
    /// Application layer request container
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class Request<TRequest, TResponse> : IRequest<Response<TResponse>>, IApplicationRequest<TRequest>
    {
        public TRequest Data { get; }
        public ClaimsPrincipal Principal { get; }

        public Request(TRequest data, ClaimsPrincipal claimsPrincipal)
        {
            Data = data;
            Principal = claimsPrincipal;
        }
    }
}
