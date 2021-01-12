using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class LoginUserInput
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public LoginUserInput(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
    public sealed class LoginUserOutput
    {
        public string AccessToken { get; }
        public LoginUserOutput(string accessToken) => AccessToken = accessToken;
    }
    public interface ILoginUserOutputPort : IOutputPortStandard<LoginUserOutput>, IOutputPortError { }
    public interface ILoginUserUseCase : IUseCase<LoginUserInput> { }
}
