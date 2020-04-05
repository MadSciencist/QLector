using MediatR;
using Microsoft.Extensions.Logging;
using QLector.Application.Commands;
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
            _logger.LogInformation("----- Handling command {Command}", request.GetType().Name);

            if(request is Request<TRequest, TResponse> command)
            {
                _logger.LogDebug("{@data}", command.Data);
            }

            try
            {
                var response = await next();
                _logger.LogInformation("----- Command {Command} handled", request.GetType().Name);
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
