using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLector.Domain.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLector.Security
{
    public class JwtTokenBuilder : ITokenBuilder
    {
        private readonly TokenOptionsSection _options;

        public JwtTokenBuilder(IOptions<TokenOptionsSection> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }
        
        public (string token, DateTime expires) Build(User user, IEnumerable<Claim> claims)
        {
            var rawKey = _options.Key ?? throw new ArgumentNullException("Key not found in config!", "key");
            var issuer = _options.Issuer ?? throw new ArgumentNullException("Issuer not found in config!", "issuer");
            var audience = _options.Audience ?? throw new ArgumentNullException("Audience not found in config!", "audience");
            var tokenValidTime = _options.TokenLifetimeMinutes;

            if (tokenValidTime == 0)
                throw new ArgumentNullException("Token valid time cannot be 0! (is it in config?)", "tokenValidTimeMin");
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(rawKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(tokenValidTime);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims ?? new List<Claim>(),
                expires: expires,
                signingCredentials: signingCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
