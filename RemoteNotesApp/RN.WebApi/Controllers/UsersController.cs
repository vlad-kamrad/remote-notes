using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;
using RN.Application.UseCases.User.Commands.ChangeRole;
using RN.Application.UseCases.User.Commands.UpdateUser;
using RN.Application.UseCases.User.Querie.GetRoles;
using RN.Application.UseCases.User.Querie.GetUserInfo;
using RN.Application.UseCases.User.Querie.GetUsers;
using RN.Domain;
using RN.Domain.Entities;
using RN.WebApi.DTO;
using RN.WebApi.Presenters.User;
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
        public async Task<IActionResult> GetUsers(
            [FromServices] IGetUsersUseCase useCase, 
            [FromServices] GetUsersPresenter presenter)
        {
            await useCase.Execute(new GetUsersInput());
            return presenter.ViewModel;
        }

        [HttpGet("roles")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAppRoles(
            [FromServices] IGetRolesUseCase useCase,
            [FromServices] GetRolesPresenter presenter)
        {
            await useCase.Execute(new GetRolesInput());
            return presenter.ViewModel;
        }

        [HttpPost("change-roles")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ChangeUserRole(
            [FromServices] IChangeRoleUseCase useCase, 
            [FromServices] ChangeRolePresenter presenter, 
            [FromBody] ChangeRoleDto input)
        {
            await useCase.Execute(new ChangeRoleInput(input.UserId, input.DesiredRoles));
            return presenter.ViewModel;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(
            [FromServices] IUpdateUserUseCase useCase,
            [FromServices] UpdateUserPresenter presenter,
            [FromBody] InputUserDto input)
        {
            await useCase.Execute(new UpdateUserInput(
                input.Name,
                input.Surname, 
                input.Email,
                input.Password));
            return presenter.ViewModel;
        }

        [HttpGet("information")]
        [Authorize]
        public async Task<IActionResult> GetUserInformation(
            [FromServices] IGetUserInfoUseCase useCase,
            [FromServices] GetUserInfoPresenter presenter)
        {
            await useCase.Execute(new GetUserInfoInput());
            return presenter.ViewModel;
        }
    }
}
