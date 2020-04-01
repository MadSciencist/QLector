using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("----- Handling command {Command} ({@Request})", request.GetType().Name, request);

            try
            {
                var response = await next();
                _logger.LogInformation("----- Command {Command} handled - response: {@Response}", request.GetType().Name, response);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "---- Unhandled exception in behavior pipeline -----");
                throw;
            }
        }
    }
}
