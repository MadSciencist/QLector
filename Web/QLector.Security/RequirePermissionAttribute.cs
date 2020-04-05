using System;

namespace QLector.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RequirePermissionAttribute : Attribute
    {
        public string PermissionName { get; }

        public RequirePermissionAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
