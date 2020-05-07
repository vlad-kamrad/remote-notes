/*using MediatR;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using RN.Domain;
using RN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var isAdmin = currentUser.Roles.Select(x => x.Name).Contains(Roles.Admin);
            if (!isAdmin) throw new BadRequestException();   // TODO: Change to other excep 

            var user = await identityService.FindByIdAsync(request.UserId);
            if (user == null) throw new NotFoundException("User", request.UserId);

            var userRoles = identityService.GetUserRoles(request.UserId).Select(x => x.Name);
            var desiredRoles = request.DesiredRoles;

            var toRemove = userRoles.Where(x => !desiredRoles.Contains(x));
            var toAdd = desiredRoles.Where(x => !userRoles.Contains(x));

            foreach (var role in toRemove) await identityService.RemoveRole(user.Id, role);
            foreach (var role in toAdd) await identityService.AddRole(user.Id, role);

            return null;
        }
    }
}
*/