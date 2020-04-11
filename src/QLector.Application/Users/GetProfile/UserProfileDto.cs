using System;

namespace QLector.Application.Users.GetProfile
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? LastLogged { get; set;  }
    }
}
