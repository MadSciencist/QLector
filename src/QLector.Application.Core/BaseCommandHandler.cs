using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Core
{
    public abstract class
        BaseCommandHandler<TRequest, TResponse> : IRequestHandler<CommandRequest<TRequest, TResponse>,
            Response<TResponse>> where TRequest : ICommand
    {
        protected IServiceProvider Services { get; }

        private IMapper _mapper;
        protected IMapper Mapper  => _mapper ??= Services.GetRequiredService<IMapper>();

        private ILogger<BaseCommandHandler<TRequest, TResponse>> _logger;
        protected ILogger<BaseCommandHandler<TRequest, TResponse>> Logger
            => _logger ??= Services.GetRequiredService<ILogger<BaseCommandHandler<TRequest, TResponse>>>();

        protected CancellationToken HandlerCancellationToken { get; private set; }

        protected BaseCommandHandler(IServiceProvider services)
        {
            Services = services;
        }

        public async Task<Response<TResponse>> Handle(CommandRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            HandlerCancellationToken = cancellationToken;
            return await Handle(request);
        }

        protected abstract Task<Response<TResponse>> Handle(CommandRequest<TRequest, TResponse> request);
    }
}
