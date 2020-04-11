using FluentValidation;

namespace QLector.Application.Users.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}
