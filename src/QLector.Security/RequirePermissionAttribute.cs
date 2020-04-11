using System;

namespace QLector.Security
{
    /// <summary>
    /// Authorization attribute. Marks class with given permission requirement.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RequirePermissionAttribute : Attribute
    {
        public string PermissionName { get; }

        public RequirePermissionAttribute(string role)
        {
            PermissionName = role;
        }
    }
}
