using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class UpdateAccessTokenUseCase : IUpdateAccessTokenUseCase
    {
        private readonly IAuthService authService;
        private readonly IIdentityService identityService;
        private readonly IUpdateAccessTokenOutputPort outputPort;
        public UpdateAccessTokenUseCase(
            IAuthService authService,
            IIdentityService identityService,
            IUpdateAccessTokenOutputPort outputPort)
        {
            this.authService = authService;
            this.outputPort = outputPort;
            this.identityService = identityService;
        }
        public async Task Execute(UpdateAccessTokenInput input)
        {
            var userId = await identityService.GetUserIdAsync(input.UserName);
            var userRoles = await identityService.GetUserRoles(userId);
            if (userRoles.Count == 0)
            {
                outputPort.WriteError("You are locked");
                return;
            }

            var accessToken = await authService.GenerateRefrershToken(input.RefreshToken, input.UserName);
            outputPort.Standart(new UpdateAccessTokenOutput(accessToken));
        }
    }
}
