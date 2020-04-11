using QLector.Domain.Core;

namespace QLector.Domain.Users
{
    public class UserRoleLink : IEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
