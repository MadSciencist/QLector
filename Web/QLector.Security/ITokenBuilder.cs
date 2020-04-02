using QLector.Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace QLector.Security
{
    public interface ITokenBuilder
    {
        (string token, DateTime expires) Build(User user, IEnumerable<Claim> claims);
    }
}