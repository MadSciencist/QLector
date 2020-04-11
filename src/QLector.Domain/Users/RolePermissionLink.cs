using QLector.Domain.Core;

namespace QLector.Domain.Users
{
    public class RolePermissionLink : IEntity
    {
        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
