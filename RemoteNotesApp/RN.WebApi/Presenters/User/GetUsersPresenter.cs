using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;

namespace RN.WebApi.Presenters.User
{
    public class GetUsersPresenter : IGetUsersOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(GetUsersOutput output) => ViewModel = new OkObjectResult(new { Users = output.Users });
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
