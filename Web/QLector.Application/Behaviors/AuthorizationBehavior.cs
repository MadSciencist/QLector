using MediatR;
using Microsoft.Extensions.Logging;
using QLector.Application.Commands;
using QLector.Security;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<AuthorizationBehavior<TRequest, TResponse>> _logger;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationBehavior(ILogger<AuthorizationBehavior<TRequest, TResponse>> logger, IAuthorizationService authorizationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var handlerType = GetHandlerType();
            var permissionAttribute = handlerType.GetCustomAttribute<RequirePermissionAttribute>(false);

            if (permissionAttribute != null && !string.IsNullOrWhiteSpace(permissionAttribute.PermissionName))
            {
                _logger.LogInformation("Evaluating permission {permission}", permissionAttribute.PermissionName);

                if (!(request is Request<TRequest, TResponse> command))
                    throw new InvalidOperationException($"Command must be {nameof(Request<TRequest, TResponse>)} class!");

                if (!_authorizationService.Authorize(command.Principal, permissionAttribute.PermissionName))
                {
                    _logger.LogError("Authorization failed!");
                    throw new UnauthorizedAccessException("Current principal has no access to given ressource");
                }
                else
                {
                    _logger.LogInformation("Authorization successful");
                }

            }
            else
            {
                _logger.LogWarning("Handler {handler} has no PermissionAttribute", handlerType);
            }

            return await next();
        }

        private Type GetHandlerType()
        {
            var handlers = this.GetType()
                .Assembly.GetTypes()
                .Where(type => typeof(IRequestHandler<TRequest, TResponse>).IsAssignableFrom(type))
                .ToList();

            if (handlers is null || handlers.Count != 1)
                throw new InvalidOperationException("Exactly one command handler is needed");

            return handlers.First();
        }
    }
}
