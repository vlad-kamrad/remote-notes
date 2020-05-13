using MediatR;

namespace RN.Application.UseCases.User.Querie.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserInfoVm> { }
}
