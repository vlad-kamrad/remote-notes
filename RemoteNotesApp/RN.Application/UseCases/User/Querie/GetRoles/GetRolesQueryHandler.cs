using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using RN.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Querie.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RolesVm>
    {
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;
        public GetRolesQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService, ICurrentUserService currentUser)
        {
            this.identityService = identityService;
            this.context = context;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }

        public async Task<RolesVm> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUser.UserId;
            if (!await identityService.CheckRole(currentUserId, Roles.Admin))
            {
                throw new BadRequestException("You role is not Administrator");
            }

            var roles = await context.Roles
                .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new RolesVm() { Roles = roles };
        }
    }
}
