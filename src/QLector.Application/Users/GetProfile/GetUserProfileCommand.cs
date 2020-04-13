using QLector.Application.Core;
using QLector.Security;

namespace QLector.Application.Users.GetProfile
{
    [PermitOnlyUserItself]
    public class GetUserProfileCommand : ICommand
    {
        [IsUserIdentifier]
        public int UserId { get; set; }
    }
}
