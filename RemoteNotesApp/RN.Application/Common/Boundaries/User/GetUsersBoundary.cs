using RN.Application.Common.Boundaries.Interfaces;
using RN.Application.UseCases.User.Querie.GetUsers;
using RN.Domain.Common;
using System.Collections.Generic;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class GetUsersInput { }
    public sealed class GetUsersOutput
    {
        public IList<UserDto> Users { get; }
        public GetUsersOutput(IList<UserDto> users) => Users = users;
    }
    public interface IGetUsersOutputPort : IOutputPortStandard<GetUsersOutput>, IOutputPortError { }
    public interface IGetUsersUseCase : IUseCase<GetUsersInput> { }
}
