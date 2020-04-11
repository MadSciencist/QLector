using MediatR;
using Microsoft.Extensions.Logging;
using QLector.Security;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Core.Behaviors
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
            if (!(request is IApplicationRequest<object> appRequest))
                throw new ArgumentNullException($"{nameof(appRequest)} app request must implement {nameof(IApplicationRequest<object>)}");

            var requestType = appRequest.Data?.GetType();

            ProcessOnlyUserItselfPermission(appRequest, requestType);
            ProcessRequirePermissionAttribute(appRequest, requestType);

            return await next();
        }

        private void ProcessOnlyUserItselfPermission(IApplicationRequest<object> appRequest, Type handlerType)
        {
            var userItselfAttribute = handlerType.GetCustomAttribute<PermitOnlyUserItselfAttribute>(false);
            if (userItselfAttribute is null) return;

            _logger.LogInformation($"Evaluating {nameof(PermitOnlyUserItselfAttribute)}");

            var commandType = appRequest.Data.GetType();

            var idProperty = commandType.GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute<IsUserIdentifierAttribute>(false) != null);

            if (idProperty is null)
            {
                var message = $"When using {nameof(PermitOnlyUserItselfAttribute)}, the command must have {nameof(IsUserIdentifierAttribute)} indicating user ID";
                throw new ArgumentNullException(message);
            }

            var userId = idProperty.GetValue(appRequest.Data);

            if(userId is null)
                throw new ArgumentNullException("User identifier is not provided!");

            if (!_authorizationService.HasPrincipalClaimedIdentifier(appRequest.Principal, userId))
            {
                _logger.LogError("Authorization failed!");
                throw new UnauthorizedAccessException("Current principal has no access to given resource - not an owner");
            }
        }

        private void ProcessRequirePermissionAttribute(IApplicationRequest<object> appRequest, Type requestType)
        {
            var permissionAttribute = requestType.GetCustomAttribute<RequirePermissionAttribute>(false);
            if (permissionAttribute != null && !string.IsNullOrWhiteSpace(permissionAttribute.PermissionName))
            {
                _logger.LogInformation("Evaluating permission {permission}", permissionAttribute.PermissionName);


                if (!_authorizationService.AuthorizeByRole(appRequest.Principal, permissionAttribute.PermissionName))
                {
                    _logger.LogError("Authorization failed!");
                    throw new UnauthorizedAccessException("Current principal has no access to given resource - missing permissions");
                }

                _logger.LogInformation("Authorization successful");
            }
            else
            {
                _logger.LogWarning("Command {command} has no RequirePermission attribute", requestType);
            }
        }
    }
}
