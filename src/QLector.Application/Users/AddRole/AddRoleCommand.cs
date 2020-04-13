using QLector.Application.Core;

namespace QLector.Application.Users.AddRole
{
    public class AddRoleCommand : ICommand
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
