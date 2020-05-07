using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RN.Application.UseCases.User.Commands.CreateUser;
using RN.Application.UseCases.User.Commands.LoginUser;
using RN.Application.UseCases.User.Commands.UpdateAccessToken;
using System.Threading.Tasks;

namespace RN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        [HttpPost("token")]
        public async Task<string> Login([FromBody] LoginUserCommand loginUserCommand) =>
            await Mediator.Send(loginUserCommand);

        [HttpPost("refresh")]
        public async Task<string> UpdateAccessToken([FromBody] UpdateAccessTokenCommand updateAccessTokenCommand) =>
            await Mediator.Send(updateAccessTokenCommand);

        [HttpPost("register")]
        public async Task<string> Register([FromBody] CreateUserCommand createUserCommand) =>
            await Mediator.Send(createUserCommand);

        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [HttpGet("protected")]
        public async Task<string> Protected() => "Welcome";
    }
}
