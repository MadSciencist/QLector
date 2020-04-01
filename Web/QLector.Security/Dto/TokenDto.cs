using System;

namespace QLector.Security.Dto
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ValidTo{ get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
