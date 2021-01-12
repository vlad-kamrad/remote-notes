using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using RN.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class ChangeRoleUseCase : IChangeRoleUseCase
    {
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        private readonly IChangeRoleOutputPort outputPort;
        public ChangeRoleUseCase(
            ICurrentUserService currentUser,
            IIdentityService identityService,
            IChangeRoleOutputPort outputPort)
        {
            this.currentUser = currentUser;
            this.identityService = identityService;
            this.outputPort = outputPort;
        }

        public async Task Execute(ChangeRoleInput input)
        {
            var currentUserId = currentUser.UserId;
            if (!await identityService.CheckRole(currentUserId, Roles.Admin))
            {
                outputPort.WriteError("You role is not Administrator");
                return;
            }

            var user = await identityService.FindByIdAsync(input.UserId);
            if (user == null)
            {
                outputPort.WriteError("User not found");
                return;
            }

            var _ = await identityService.GetUserRoles(input.UserId);
            var userRoles = _.Select(x => x.Name);
            var desiredRoles = input.DesiredRoles;

            var toRemove = userRoles.Where(x => !desiredRoles.Contains(x));
            var toAdd = desiredRoles.Where(x => !userRoles.Contains(x));

            foreach (var role in toRemove) await identityService.RemoveRole(user.Id, role);
            foreach (var role in toAdd) await identityService.AddRole(user.Id, role);

            var outputRoles = await identityService.GetUserRoles(input.UserId);
            outputPort.Standart(new ChangeRoleOutput(outputRoles));
        }
    }
}
