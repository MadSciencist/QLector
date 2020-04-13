using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace QLector.Application.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidationBehavior(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!(request is IApplicationRequest<object> appRequest))
                throw new ArgumentNullException($"{nameof(appRequest)} app request must implement {nameof(IApplicationRequest<object>)}");

            var commandType = appRequest.Data.GetType();
            var validator = _validatorFactory.GetValidator(commandType);

            if (validator != null)
            {
                var result = validator.Validate(new ValidationContext(appRequest.Data));

                if (!result.IsValid)
                    throw new ValidationException(result.Errors);
            }

            return await next();
        }
    }
}
