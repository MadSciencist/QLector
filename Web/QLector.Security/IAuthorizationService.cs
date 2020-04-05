using System.Security.Claims;

namespace QLector.Security
{
    public interface IAuthorizationService
    {
        bool Authorize(ClaimsPrincipal principal, string permission);
    }
}
