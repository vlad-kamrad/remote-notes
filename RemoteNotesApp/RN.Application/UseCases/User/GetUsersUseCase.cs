using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Boundaries.User;
using RN.Application.Common.Interfaces;
using RN.Application.UseCases.User.Querie.GetUsers;
using RN.Domain;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User
{
    public class GetUsersUseCase : IGetUsersUseCase
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IIdentityService identityService;
        private readonly IGetUsersOutputPort outputPort;
        private readonly IMapper mapper;
        public GetUsersUseCase(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            IGetUsersOutputPort outputPort,
            IIdentityService identityService,
            IMapper mapper)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.identityService = identityService;
            this.outputPort = outputPort;
            this.mapper = mapper;
        }
        public async Task Execute(GetUsersInput input)
        {
            var currentUserId = currentUser.UserId;
            if (!await identityService.CheckRole(currentUserId, Roles.Admin))
            {
                outputPort.WriteError("You role is not Administrator");
                return;
            }

            var users = await context.Users
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                //.OrderBy(x => x.Email)
                .ToListAsync();
            outputPort.Standart(new GetUsersOutput(users));
            //return new UsersVm { Users = users };
        }
    }
}
