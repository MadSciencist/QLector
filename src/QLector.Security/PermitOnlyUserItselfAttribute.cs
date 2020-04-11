using System;

namespace QLector.Security
{
    /// <summary>
    /// Authorization attribute.
    /// Permits only current principal
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class PermitOnlyUserItselfAttribute : Attribute
    {
        /// <summary>
        /// Type of attribute indicating userId on the command.
        /// Defaults to typeof(IsUserIdentifierAttribute)
        /// </summary>
        public Type UserIdentifierMarkerAttributeType { get; }

        public PermitOnlyUserItselfAttribute(Type userIdentifierMarkerAttributeType = null)
        {
            UserIdentifierMarkerAttributeType = userIdentifierMarkerAttributeType ?? typeof(IsUserIdentifierAttribute);
        }
    }
}
