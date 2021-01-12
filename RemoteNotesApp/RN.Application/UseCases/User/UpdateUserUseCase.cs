using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        private readonly IUpdateUserOutputPort outputPort;
        public UpdateUserUseCase(
            ICurrentUserService currentUser,
            IIdentityService identityService,
            IUpdateUserOutputPort outputPort)
        {
            this.currentUser = currentUser;
            this.outputPort = outputPort;
            this.identityService = identityService;
        }

        public async Task Execute(UpdateUserInput input)
        {
            var user = new Domain.Entities.User()
            {
                Id = currentUser.UserId,
                Name = input.Name,
                Surname = input.Surname,
                Email = input.Email,
                Password = input.Password
            };

            bool success = await identityService.UpdateUserAsync(user);
            outputPort.Standart(new UpdateUserOutput(success));
        }
    }
}
