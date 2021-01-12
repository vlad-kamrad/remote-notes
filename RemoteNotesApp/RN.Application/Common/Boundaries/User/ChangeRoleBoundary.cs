using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;
using RN.Domain.Entities;
using System.Collections.Generic;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class ChangeRoleInput
    {
        public string UserId { get; set; }
        public List<string> DesiredRoles { get; set; }
        public ChangeRoleInput(string userId, List<string> desiredRoles)
        {
            UserId = userId;
            DesiredRoles = desiredRoles;
        }
    }
    public sealed class ChangeRoleOutput
    {
        public List<Role> Roles { get; }
        public ChangeRoleOutput(List<Role> roles) => Roles = roles;
    }
    public interface IChangeRoleOutputPort : IOutputPortStandard<ChangeRoleOutput>, IOutputPortError { }
    public interface IChangeRoleUseCase : IUseCase<ChangeRoleInput> { }
}
