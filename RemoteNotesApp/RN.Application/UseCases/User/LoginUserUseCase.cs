using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IIdentityService identityService;
        private readonly IAuthService authService;
        private readonly ILoginUserOutputPort outputPort;
        public LoginUserUseCase(
            IIdentityService identityService,
            IAuthService authService,
            ILoginUserOutputPort outputPort)
        {
            this.identityService = identityService;
            this.outputPort = outputPort;
            this.authService = authService;
        }
        public async Task Execute(LoginUserInput input)
        {
            var userId = await identityService.GetUserIdAsync(input.Name);

            var userRoles = await identityService.GetUserRoles(userId);
            if (userRoles.Count == 0)
            {
                outputPort.WriteError("You are locked");
                return;
            }

            if (await identityService.CheckPassword(userId, input.Password))
            {
                string response = await authService.GenerateAccessToken(input.Name, input.Password);

                if (response != null)
                {
                    outputPort.Standart(new LoginUserOutput(response));
                    return;
                }

                outputPort.WriteError("genarete token error");
                return;
            }

            outputPort.WriteError("not found error");
        }
    }
}
