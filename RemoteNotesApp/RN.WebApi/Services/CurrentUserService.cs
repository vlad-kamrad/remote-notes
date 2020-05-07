using Microsoft.AspNetCore.Http;
using RN.Application.Common.Interfaces;
using RN.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RN.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor/*, IIdentityService identityService*/)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
