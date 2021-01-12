using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;
using RN.Application.UseCases.User.Commands.CreateUser;
using RN.Application.UseCases.User.Commands.LoginUser;
using RN.Application.UseCases.User.Commands.UpdateAccessToken;
using RN.WebApi.DTO;
using RN.WebApi.Presenters.User;
using System.Threading.Tasks;

namespace RN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        [HttpPost("token")]
        public async Task<IActionResult> Login(
            [FromServices] ILoginUserUseCase useCase,
            [FromServices] LoginUserPresenter presenter, 
            [FromBody] LoginParams input)
        {
            await useCase.Execute(new LoginUserInput(input.Name, input.Password));
            return presenter.ViewModel;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> UpdateAccessToken(
            [FromServices] IUpdateAccessTokenUseCase useCase,
            [FromServices] UpdateAccessTokenPresenter presenter, 
            [FromBody] UpdateAccessTokenDto input)
        {
            await useCase.Execute(new UpdateAccessTokenInput(input.RefreshToken, input.UserName));
            return presenter.ViewModel;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromServices] ICreateUserUseCase useCase,
            [FromServices] CreateUserPresenter presenter,
            [FromBody] InputUserDto input)
        {
            await useCase.Execute(new CreateUserInput(
                input.Name, input.Surname, input.Email, input.Password));
            return presenter.ViewModel;
        }

        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [HttpGet("protected")]
        public async Task<string> Protected() => "Welcome";
    }
}
