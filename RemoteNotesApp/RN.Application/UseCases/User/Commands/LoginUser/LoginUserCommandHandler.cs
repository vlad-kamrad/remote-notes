using MediatR;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IIdentityService identityService;
        private readonly IAuthService authService;

        public LoginUserCommandHandler(IIdentityService identityService, IAuthService authService)
        {
            this.identityService = identityService;
            this.authService = authService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.GetUserIdAsync(request.Name);

            if (await identityService.CheckPassword(userId, request.Password))
            {
                string response = await authService.GenerateAccessToken(request.Name, request.Password);
                
                if (response != null)
                {
                    return response;
                }

                throw new Exception("genarete token");
            }

            throw new NotFoundException(request.Name, request);
        }
    }
}
