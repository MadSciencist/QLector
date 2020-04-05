using System;

namespace QLector.Security.Exceptions
{
    public class InvalidLoginAttemptException : Exception
    {
        public InvalidLoginAttemptException()
        { }

        public InvalidLoginAttemptException(string message)
            : base(message)
        { }

        public InvalidLoginAttemptException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
