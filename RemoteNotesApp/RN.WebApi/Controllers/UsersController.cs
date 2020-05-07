using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RN.Application.UseCases.User.Querie.GetUsers;
using RN.Domain;
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
    }
}
