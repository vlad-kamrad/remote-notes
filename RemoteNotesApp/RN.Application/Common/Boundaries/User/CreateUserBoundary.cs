using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class CreateUserInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateUserInput(
            string name,
            string surname,
            string email,
            string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
        }
    }
    public sealed class CreateUserOutput
    {
        public string AccessToken { get; }
        public CreateUserOutput(string accessToken) => AccessToken = accessToken;
    }
    public interface ICreateUserOutputPort : IOutputPortStandard<CreateUserOutput>, IOutputPortError { }
    public interface ICreateUserUseCase : IUseCase<CreateUserInput> { }
}
