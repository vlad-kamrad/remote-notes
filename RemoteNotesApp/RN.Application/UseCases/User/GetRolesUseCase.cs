using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using RN.Application.UseCases.User.Querie.GetRoles;
using RN.Domain;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class GetRolesUseCase : IGetRolesUseCase
    {
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        private readonly IGetRolesOutputPort outputPort;
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        public GetRolesUseCase(
            IIdentityService identityService,
            ICurrentUserService currentUser,
            IApplicationDbContext context,
            IGetRolesOutputPort outputPort,
            IMapper mapper)
        {
            this.identityService = identityService;
            this.context = context;
            this.mapper = mapper;
            this.currentUser = currentUser;
            this.outputPort = outputPort;
        }
        public async Task Execute(GetRolesInput input)
        {
            var currentUserId = currentUser.UserId;
            if (!await identityService.CheckRole(currentUserId, Roles.Admin))
            {
                outputPort.WriteError("You role is not Administrator");
                return;
            }

            var roles = await context.Roles
                .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            outputPort.Standart(new GetRolesOutput(roles));
            // return new RolesVm() { Roles = roles };
        }
    }
}
