using FluentValidation.Results;
using QLector.Domain.Core;

namespace QLector.Application.Core.Extensions
{
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Converts ValidationFailure into Message
        /// </summary>
        /// <param name="validationFailure"></param>
        /// <returns></returns>
        public static Message ToResponseMessage(this ValidationFailure validationFailure) =>
            new Message
            {
                Type = MessageType.ValidationError, Value = validationFailure.ErrorMessage
            };
    }
}
