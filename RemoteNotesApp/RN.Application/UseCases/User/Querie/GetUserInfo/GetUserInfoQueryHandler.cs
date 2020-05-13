using MediatR;
using RN.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.User.Querie.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoVm>
    {
        private readonly IIdentityService identityService;
        private readonly ICurrentUserService currentUser;
        public GetUserInfoQueryHandler(ICurrentUserService currentUser, IIdentityService identityService)
        {
            this.currentUser = currentUser;
            this.identityService = identityService;
        }
        public async Task<UserInfoVm> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await identityService.FindByIdAsync(currentUser.UserId);
            return new UserInfoVm()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Created = user.Created,
                Modified = user.LastModified
            };
        }
    }
}
