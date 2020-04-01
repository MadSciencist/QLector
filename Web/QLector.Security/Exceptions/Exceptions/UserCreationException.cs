using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace QLector.Security.Exceptions.Exceptions
{
    public class UserCreationException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; }

        public UserCreationException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }

        public UserCreationException()
        { }

        public UserCreationException(string message)
            : base(message)
        { }

        public UserCreationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
