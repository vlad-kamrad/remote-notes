using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RN.Application.UseCases.User.Commands.ChangeRole;
using RN.Application.UseCases.User.Commands.UpdateUser;
using RN.Application.UseCases.User.Querie.GetRoles;
using RN.Application.UseCases.User.Querie.GetUserInfo;
using RN.Application.UseCases.User.Querie.GetUsers;
using RN.Domain;
using RN.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<UsersVm> GetUsers() =>
            await Mediator.Send(new GetUsersQuery());

        [HttpGet("roles")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<RolesVm> GetAppRoles() =>
            await Mediator.Send(new GetRolesQuery());

        [HttpPost("change-roles")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<List<Role>> ChangeUserRole(ChangeRoleCommand changeRoleCommand) =>
            await Mediator.Send(changeRoleCommand);

        [HttpPut]
        [Authorize]
        public async Task<bool> UpdateUserInfo(UpdateUserCommand updateUserCommand) =>
            await Mediator.Send(updateUserCommand);

        [HttpGet("information")]
        [Authorize]
        public async Task<UserInfoVm> GetUserInformation() =>
            await Mediator.Send(new GetUserInfoQuery());

        //public async Task<>
    }
}
