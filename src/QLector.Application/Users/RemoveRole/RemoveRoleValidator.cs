using FluentValidation;

namespace QLector.Application.Users.RemoveRole
{
    public class RemoveRoleValidator : AbstractValidator<RemoveRoleCommand>
    {
        public RemoveRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
