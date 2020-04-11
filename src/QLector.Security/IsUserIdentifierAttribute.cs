using System;

namespace QLector.Security
{
    [AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IsUserIdentifierAttribute : Attribute
    {
    }
}
