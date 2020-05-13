using MediatR;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Commands.UpdateAccessToken
{
    public class UpdateAccessTokenHandler : IRequestHandler<UpdateAccessTokenCommand, string>
    {
        private readonly IAuthService authService;
        private readonly IIdentityService identityService;
        public UpdateAccessTokenHandler(IAuthService authService, IIdentityService identityService)
        {
            this.authService = authService;
            this.identityService = identityService;
        }

        public async Task<string> Handle(UpdateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdAsync(request.UserName);
            var userRoles = await identityService.GetUserRoles(userId);
            if (userRoles.Count == 0)
            {
                throw new LockedRequestException("You are locked");
            }

            return await authService.GenerateRefrershToken(request.RefreshToken, request.UserName);
        }
    }
}
