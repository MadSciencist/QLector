using System;
using System.ComponentModel.DataAnnotations;

namespace QLector.Security.Exceptions
{
    public class UserAlreadyExistsException : ValidationException
    {
        public UserAlreadyExistsException()
        { }

        public UserAlreadyExistsException(string message)
            : base(message)
        { }

        public UserAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
