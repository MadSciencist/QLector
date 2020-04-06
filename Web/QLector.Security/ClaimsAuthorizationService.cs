using QLector.Entities.Enumerations.Users;
using System;
using System.Security.Claims;

namespace QLector.Security
{
    public class ClaimsAuthorizationService : IAuthorizationService
    {
        public bool AuthorizeByRole(ClaimsPrincipal principal, string role)
        {
            if (principal is null)
                return false;

            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException(nameof(role));

            // Allow admin
            return principal.IsInRole(Roles.AdminUser) || principal.IsInRole(role);
        }
    }
}
