using System;

namespace QLector.Application.Users.Login
{
    public class UserLoggedDto
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
