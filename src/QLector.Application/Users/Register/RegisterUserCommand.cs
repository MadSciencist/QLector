using QLector.Application.Core;

namespace QLector.Application.Users.Register
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName{ get; set; }
        public string Password { get; set; }
        public string Email{ get; set; }
    }
}
