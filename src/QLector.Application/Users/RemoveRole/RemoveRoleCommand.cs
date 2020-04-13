using QLector.Application.Core;

namespace QLector.Application.Users.RemoveRole
{
    //[RequirePermission("Users.ManageRoles")]
    public class RemoveRoleCommand : ICommand
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
