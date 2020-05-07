using MediatR;

namespace RN.Application.UseCases.User.Querie.GetUsers
{
    public class GetUsersQuery : IRequest<UsersVm> { }
}
