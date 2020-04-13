using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QLector.Application.Core.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Core
{
    public abstract class
        BaseQueryHandler<TRequest, TResponse> : IRequestHandler<PagedQueryRequest<TRequest, TResponse>,
            PagedResponse<TResponse>> where TRequest : IQuery
    {
        protected IServiceProvider Services { get; }

        private IDbConnectionFactory _dbConnectionFactory;
        protected IDbConnectionFactory DbConnectionFactory
            => _dbConnectionFactory ??= Services.GetRequiredService<IDbConnectionFactory>();

        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= Services.GetRequiredService<IMapper>();

        private ILogger<BaseQueryHandler<TRequest, TResponse>> _logger;
        protected ILogger<BaseQueryHandler<TRequest, TResponse>> Logger
            => _logger ??= Services.GetRequiredService<ILogger<BaseQueryHandler<TRequest, TResponse>>>();

        protected CancellationToken HandlerCancellationToken { get; private set; }

        protected BaseQueryHandler(IServiceProvider services)
        {
            Services = services;
        }

        public async Task<PagedResponse<TResponse>> Handle(PagedQueryRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            HandlerCancellationToken = cancellationToken;
            return await Handle(request);
        }

        protected abstract Task<PagedResponse<TResponse>> Handle(PagedQueryRequest<TRequest, TResponse> request);
    }
}