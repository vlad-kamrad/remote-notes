using RN.Application.Common.Boundaries.Interfaces;
using RN.Application.UseCases.User.Querie.GetRoles;
using RN.Domain.Common;
using System.Collections.Generic;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class GetRolesInput { }
    public sealed class GetRolesOutput
    {
        public IList<RoleDto> Roles { get; }
        public GetRolesOutput(IList<RoleDto> roles) => Roles = roles;
    }
    public interface IGetRolesOutputPort : IOutputPortStandard<GetRolesOutput>, IOutputPortError { }
    public interface IGetRolesUseCase : IUseCase<GetRolesInput> { }
}
