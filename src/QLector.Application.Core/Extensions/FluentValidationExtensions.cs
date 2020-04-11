using FluentValidation.Results;
using QLector.Domain.Core;

namespace QLector.Application.Core.Extensions
{
    public static class FluentValidationExtensions
    {
        public static Message ToResponseMessage(this ValidationFailure validationFailure) =>
            new Message
            {
                Type = MessageType.ValidationError, Value = validationFailure.ErrorMessage
            };
    }
}
