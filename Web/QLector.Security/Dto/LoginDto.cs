namespace QLector.Security.Dto
{
    public class LoginDto
    {
        public string Login { get; }
        public string Password { get; }

        public LoginDto(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public LoginDto()
        {
        }
    }
}
