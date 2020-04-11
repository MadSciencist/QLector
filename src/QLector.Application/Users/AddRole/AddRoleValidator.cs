using FluentValidation;

namespace QLector.Application.Users.AddRole
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
}