using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using RN.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Querie.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersVm>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        public GetUsersQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUser.UserId;
            if (!await identityService.CheckRole(currentUserId, Roles.Admin))
            {
                throw new BadRequestException("You role is not Administrator");
            }

           /* var users = await context.Users.AsNoTracking().Select(x => new
            {
                Roles = identityService.GetUserRoles(x.Id).Result,
                Name = x.Name,
                Id = x.Id,
                Email = x.Email,
                Surname = x.Surname
            }).ToListAsync();


            var a = users.Select(x => new UserDto
            {
                Roles = x.Roles.Select(x => x.Name).ToList(),
                Name = x.Name,
                Id = x.Id,
                Email = x.Email,
                Surname = x.Surname
            }).ToList();*/

            /*var users = context.UserRoles.GroupBy(x => x.User).SelectMany(x => new UserDto
            {
                Roles= x.
            });*/

            var users = await context.Users
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                //.OrderBy(x => x.Email)
                .ToListAsync();

            return new UsersVm { Users = users };
        }
    }
}
