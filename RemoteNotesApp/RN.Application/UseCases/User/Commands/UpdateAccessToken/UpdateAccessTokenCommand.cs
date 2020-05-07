using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RN.Application.UseCases.User.Commands.UpdateAccessToken
{
    public class UpdateAccessTokenCommand : IRequest<string>
    {
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
    }
}
