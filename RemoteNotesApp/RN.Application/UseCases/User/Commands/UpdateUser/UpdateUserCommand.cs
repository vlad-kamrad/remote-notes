using MediatR;

namespace RN.Application.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
