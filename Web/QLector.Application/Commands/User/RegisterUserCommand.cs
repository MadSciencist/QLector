using MediatR;
using QLector.Application.ResponseModels;
using QLector.Application.ResponseModels.User;

namespace QLector.Application.Commands.User
{
    public class RegisterUserCommand : IRequest<Response<UserCreatedModel>>
    {
        public string UserName{ get; set; }
        public string Password { get; set; }
        public string Email{ get; set; }
    }
}
