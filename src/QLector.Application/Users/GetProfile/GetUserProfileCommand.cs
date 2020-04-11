using QLector.Security;

namespace QLector.Application.Users.GetProfile
{
    [PermitOnlyUserItself]
    public class GetUserProfileCommand
    {
        [IsUserIdentifier]
        public int UserId { get; set; }
    }
}
