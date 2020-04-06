using System.Security.Claims;

namespace QLector.Application.Commands
{
    public abstract class RequestBase
    {
        public ClaimsPrincipal Principal { get; }

        protected RequestBase(ClaimsPrincipal claimsPrincipal)
        {
            Principal = claimsPrincipal;
        }
    }
}
