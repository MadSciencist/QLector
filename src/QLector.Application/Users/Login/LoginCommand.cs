using QLector.Application.Core;

namespace QLector.Application.Users.Login
{
    public class LoginCommand : ICommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
