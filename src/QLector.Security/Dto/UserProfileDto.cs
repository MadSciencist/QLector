using System;

namespace QLector.Security.Dto
{
    public class UserProfileDto
    {
        public UserProfileDto(int id, string userName, string email, DateTime created, DateTime? modified = null, DateTime? lastLogged = null)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Created = created;
            Modified = modified;
            LastLogged = lastLogged;
        }

        public int Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public DateTime Created { get; }
        public DateTime? Modified { get; }
        public DateTime? LastLogged { get; }
    }
}
