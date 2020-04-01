using System;

namespace QLector.Domain.Infrastructure.Exceptions
{
    public class UserNotExistsException : NotFoundException
    {
        public UserNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserNotExistsException(string message) : base(message)
        {
        }

        public UserNotExistsException()
        {
        }
    }
}
