using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class UpdateUserInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UpdateUserInput(
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
    public sealed class UpdateUserOutput
    {
        public bool Success { get; }
        public UpdateUserOutput(bool success) => Success = success;
    }
    public interface IUpdateUserOutputPort : IOutputPortStandard<UpdateUserOutput>, IOutputPortError { }
    public interface IUpdateUserUseCase : IUseCase<UpdateUserInput> { }
}
