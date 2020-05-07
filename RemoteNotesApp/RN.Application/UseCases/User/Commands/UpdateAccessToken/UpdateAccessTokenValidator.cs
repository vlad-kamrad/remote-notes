using FluentValidation;

namespace RN.Application.UseCases.User.Commands.UpdateAccessToken
{
    public class UpdateAccessTokenValidator : AbstractValidator<UpdateAccessTokenCommand>
    {
        public UpdateAccessTokenValidator()
        {
            RuleFor(x => x.UserName)
                .MinimumLength(2)
                .MaximumLength(40);

            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}
