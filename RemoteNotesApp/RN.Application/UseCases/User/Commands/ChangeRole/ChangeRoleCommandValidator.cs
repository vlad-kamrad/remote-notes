using FluentValidation;

namespace RN.Application.UseCases.User.Commands.ChangeRole
{
    public class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
    {
        public ChangeRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull();

            RuleFor(x => x.DesiredRoles)
                .NotNull();
        }
    }
}
