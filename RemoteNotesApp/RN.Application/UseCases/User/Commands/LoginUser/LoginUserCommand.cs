using MediatR;

namespace RN.Application.UseCases.User.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
