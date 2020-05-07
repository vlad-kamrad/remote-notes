using MediatR;

namespace RN.Application.UseCases.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
