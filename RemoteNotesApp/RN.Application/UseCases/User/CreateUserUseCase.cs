using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IApplicationDbContext context;
        private readonly IAuthService authService;
        private readonly IIdentityService identityService;
        private readonly ICreateUserOutputPort outputPort;
        public CreateUserUseCase(
            IApplicationDbContext context,
            IAuthService authService,
            IIdentityService identityService,
            ICreateUserOutputPort outputPort)
        {
            this.context = context;
            this.authService = authService;
            this.identityService = identityService;
            this.outputPort = outputPort;
        }

        public async Task Execute(CreateUserInput input)
        {
            if (input.Name.Length <= 2 || input.Name.Length >= 40)
            {
                outputPort.WriteError("Name must be between 2 and 40 characters"); return;
            }

            if (input.Password.Length < 6)
            {
                outputPort.WriteError("Password must be more than 6 characters"); return;
            }

            var success = await identityService.CreateUserAsync(new Domain.Entities.User
            {
                Name = input.Name,
                Surname = input.Surname,
                Email = input.Email,
                Password = input.Password
            });

            if (!success)
            {
                outputPort.WriteError("Error message");
                return;
            }

            var accessToken = await authService.GenerateAccessToken(input.Name, input.Password);
            outputPort.Standart(new CreateUserOutput(accessToken));
        }
    }
}
