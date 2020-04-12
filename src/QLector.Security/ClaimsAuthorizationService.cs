using System;
using System.Linq;
using System.Security.Claims;
using QLector.Domain.Users.Enumerations;

namespace QLector.Security
{
    public class ClaimsAuthorizationService : IAuthorizationService
    {
        public const string IdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public bool AuthorizeByRole(ClaimsPrincipal principal, string permission, bool allowAdmin = true)
        {
            if (principal is null)
                return false;

            if (string.IsNullOrWhiteSpace(permission))
                throw new ArgumentNullException(nameof(permission));

            // Allow admin
            return (allowAdmin && principal.IsInRole(Roles.AdminUser))
                || principal.Claims.Any(x => x.Type == PermissionClaims.PermissionClaimNamespace && x.Value == permission);
        }

        public bool HasPrincipalClaimedIdentifier(ClaimsPrincipal principal, object id, bool allowAdmin = true)
        {
            if (principal is null || id is null)
                return false;

            return (allowAdmin && principal.IsInRole(Roles.AdminUser))
                   || principal.Claims.Any(x => x.Type == IdClaimType && x.Value == id.ToString());
        }
    }
}
