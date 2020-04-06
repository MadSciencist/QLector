using MediatR;
using QLector.Application.ResponseModels;
using System.Security.Claims;

namespace QLector.Application.Commands
{
    /// <summary>
    /// Application layer request container
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class Request<TRequest, TResponse> : RequestBase, IRequest<Response<TResponse>>
    {
        public TRequest Data { get; }

        public Request(TRequest data, ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
            Data = data;
        }
    }
}
