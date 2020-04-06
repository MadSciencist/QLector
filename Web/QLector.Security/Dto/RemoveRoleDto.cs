namespace QLector.Security.Dto
{
    public class AddRemoveRoleDto
    {
        public int UserId { get; }
        public int RoleId { get; }

        public AddRemoveRoleDto(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
