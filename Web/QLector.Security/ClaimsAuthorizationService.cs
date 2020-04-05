using QLector.Entities.Enumerations.Users;
using System;
using System.Linq;
using System.Security.Claims;

namespace QLector.Security
{
    public class ClaimsAuthorizationService : IAuthorizationService
    {
        public bool Authorize(ClaimsPrincipal principal, string permission)
        {
            if (principal is null)
                return false;

            if (string.IsNullOrWhiteSpace(permission))
                throw new ArgumentNullException(nameof(permission));

            // Allow admin role
            if (principal.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == Roles.AdminUser))
                return true;

            // TODO custom claim type
            return principal.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == permission);
        }
    }
}
