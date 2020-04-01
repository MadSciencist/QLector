using FluentValidation.Results;
using QLector.Application.ResponseModels;

namespace QLector.Application.Extensions
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
