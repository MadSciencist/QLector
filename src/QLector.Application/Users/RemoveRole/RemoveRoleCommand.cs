using QLector.Security;

namespace QLector.Application.Users.RemoveRole
{
    //[RequirePermission("Users.ManageRoles")]
    public class RemoveRoleCommand
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
