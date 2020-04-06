using FluentValidation;
using FluentValidation.Results;
using MediatR;
using QLector.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QLector.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResp> : IPipelineBehavior<TRequest, TResp> where TRequest : IRequest<TResp>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResp> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResp> next)
        {
            var (isValid, messages) = DoValidate(request);

            if (!isValid)
                throw new ValidationException(messages);
       
            return await next();
        }

        private (bool isValid, IList<ValidationFailure> messages) DoValidate(TRequest request)
        {
            var validationContext = new ValidationContext(request);

            var messages = _validators?
                .Select(validator => validator.Validate(validationContext))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            return messages != null && messages.Any() ? (false, messages) : (true, messages);
        }
    }
}
