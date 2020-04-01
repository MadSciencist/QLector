using MediatR;
using QLector.Application.ResponseModels;

namespace QLector.Application.Commands.Login
{
    public class LoginCommand : IRequest<Response<UserLoggedResponseModel>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
