using MediatR;
using RN.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        public UpdateUserCommandHandler(ICurrentUserService currentUser, IIdentityService identityService)
        {
            this.currentUser = currentUser;
            this.identityService = identityService;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User()
            {
                Id = currentUser.UserId,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password
            };
            return await identityService.UpdateUserAsync(user);
        }
    }
}
