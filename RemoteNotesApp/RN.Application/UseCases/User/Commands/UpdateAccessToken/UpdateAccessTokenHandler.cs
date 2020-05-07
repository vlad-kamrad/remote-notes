﻿using MediatR;
using RN.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Commands.UpdateAccessToken
{
    public class UpdateAccessTokenHandler : IRequestHandler<UpdateAccessTokenCommand, string>
    {
        private readonly IAuthService authService;
        public UpdateAccessTokenHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<string> Handle(UpdateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            return await authService.GenerateRefrershToken(request.RefreshToken, request.UserName);
        }
    }
}
