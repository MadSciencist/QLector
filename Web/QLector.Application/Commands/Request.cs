using MediatR;
using QLector.Application.ResponseModels;
using System;
using System.Security.Claims;

namespace QLector.Application.Commands
{
    /// <summary>
    /// Application layer request container
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class Request<TRequest, TResponse> : IRequest<Response<TResponse>>
    {
        public TRequest Data { get; }

        private readonly ClaimsPrincipal _principal;
        public ClaimsPrincipal Principal { get => _principal ?? throw new NullReferenceException(nameof(_principal)); }

        public Request(TRequest data, ClaimsPrincipal claimsPrincipal)
        {
            Data = data;
            _principal = claimsPrincipal;
        }

        public Request(TRequest data) : this(data, null) { }
    }
}
