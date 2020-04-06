using FluentValidation;
using QLector.Application.Commands.User;

namespace QLector.Application.Validation.User
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }

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
