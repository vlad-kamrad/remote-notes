using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class GetUserInfoUseCase : IGetUserInfoUseCase
    {
        private readonly IIdentityService identityService;
        private readonly ICurrentUserService currentUser;
        private readonly IGetUserInfoOutputPort outputPort;
        public GetUserInfoUseCase(
            ICurrentUserService currentUser,
            IIdentityService identityService,
            IGetUserInfoOutputPort outputPort)
        {
            this.currentUser = currentUser;
            this.identityService = identityService;
            this.outputPort = outputPort;
        }

        public async Task Execute(GetUserInfoInput input)
        {
            var user = await identityService.FindByIdAsync(currentUser.UserId);
            outputPort.Standart(new GetUserInfoOutput(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Created,
                user.LastModified));
        }
    }
}
