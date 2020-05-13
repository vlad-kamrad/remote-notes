using FluentValidation;

namespace RN.Application.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .MaximumLength(40);

            RuleFor(x => x.Surname)
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
