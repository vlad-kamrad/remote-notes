using MediatR;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using RN.Domain;
using RN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, List<Role>>
    {
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        public ChangeRoleCommandHandler(ICurrentUserService currentUser, IIdentityService identityService)
        {
            this.currentUser = currentUser;
            this.identityService = identityService;
        }

        public async Task<List<Role>> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUser.UserId;
            if (!await identityService.CheckRole(currentUserId, Roles.Admin))
            {
                throw new BadRequestException("You role is not Administrator");
            }

            var user = await identityService.FindByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("User", request.UserId);

            var _ = await identityService.GetUserRoles(request.UserId);
            var userRoles = _.Select(x => x.Name);
            var desiredRoles = request.DesiredRoles;

            var toRemove = userRoles.Where(x => !desiredRoles.Contains(x));
            var toAdd = desiredRoles.Where(x => !userRoles.Contains(x));

            foreach (var role in toRemove) await identityService.RemoveRole(user.Id, role);
            foreach (var role in toAdd) await identityService.AddRole(user.Id, role);

            return await identityService.GetUserRoles(request.UserId);
        }
    }
}
