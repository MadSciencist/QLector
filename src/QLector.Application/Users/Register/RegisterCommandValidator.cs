using FluentValidation;

namespace QLector.Application.Users.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
