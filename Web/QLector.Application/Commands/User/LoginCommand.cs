using MediatR;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;

namespace QLector.Application.Commands.User
{
    public class LoginCommand : IRequest<Response<UserLoggedResponseModel>>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
