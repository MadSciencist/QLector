using System.Security.Claims;

namespace QLector.Security
{
    public interface IAuthorizationService
    {
        bool AuthorizeByRole(ClaimsPrincipal principal, string role, bool allowAdmin = true);
        bool HasPrincipalClaimedIdentifier(ClaimsPrincipal principal, object id, bool allowAdmin = true);
    }
}
