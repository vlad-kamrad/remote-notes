using FluentValidation;
using RN.Application.UseCases.User.Commands.LoginUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace RN.Application.UseCases.User.Commands.CreateUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .MaximumLength(40);

            RuleFor(x => x.Password)
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
