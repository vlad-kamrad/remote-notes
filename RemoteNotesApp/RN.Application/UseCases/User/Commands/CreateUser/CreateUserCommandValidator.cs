using FluentValidation;

namespace RN.Application.UseCases.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .MaximumLength(40);

            RuleFor(x => x.Surname)
                .MinimumLength(2)
                .MaximumLength(50);

            //RuleFor(x => x.Email)
            //    .EmailAddress();
        }
    }
}
