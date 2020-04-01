using System;

namespace QLector.Security.Exceptions
{
    public class UserNotExistsException : NotFoundException
    {
        public UserNotExistsException()
        { }

        public UserNotExistsException(string message)
            : base(message)
        { }

        public UserNotExistsException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
