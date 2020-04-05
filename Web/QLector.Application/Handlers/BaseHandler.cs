using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QLector.Application.Commands;
using QLector.Application.ResponseModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Handlers
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<Request<TRequest, TResponse>, Response<TResponse>>
    {
        protected IServiceProvider Services { get; }

        private IMapper _mapper;
        protected IMapper Mapper  => _mapper ??= Services.GetRequiredService<IMapper>();

        private ILogger<BaseHandler<TRequest, TResponse>> _logger;
        protected ILogger<BaseHandler<TRequest, TResponse>> Logger
            => _logger ??= Services.GetRequiredService<ILogger<BaseHandler<TRequest, TResponse>>>();

        protected CancellationToken HandlerCancellationToken { get; private set; }

        protected BaseHandler(IServiceProvider services)
        {
            Services = services;
        }

        public async Task<Response<TResponse>> Handle(Request<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            HandlerCancellationToken = cancellationToken;
            return await Handle(request);
        }

        protected abstract Task<Response<TResponse>> Handle(Request<TRequest, TResponse> request);
    }
}
