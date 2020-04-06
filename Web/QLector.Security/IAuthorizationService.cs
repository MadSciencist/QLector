﻿using System.Security.Claims;

namespace QLector.Security
{
    public interface IAuthorizationService
    {
        bool AuthorizeByRole(ClaimsPrincipal principal, string role);
    }
}
