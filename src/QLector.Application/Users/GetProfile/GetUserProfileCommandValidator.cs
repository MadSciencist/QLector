using FluentValidation;

namespace QLector.Application.Users.GetProfile
{
    public class GetUserProfileCommandValidator : AbstractValidator<GetUserProfileCommand>
    {
        public GetUserProfileCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
