using MediatR;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        public IApplicationDbContext context;
        public IAuthService authService;
        public IIdentityService identityService;
        public CreateUserCommandHandler(IApplicationDbContext context, IAuthService authService, IIdentityService identityService)
        {
            this.context = context;
            this.authService = authService;
            this.identityService = identityService;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var success = await identityService.CreateUserAsync(new Domain.Entities.User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password
            });

            if (!success)
            {
                throw new BadRequestException();
            }

            return await authService.GenerateAccessToken(request.Name, request.Password);
        }
    }
}
