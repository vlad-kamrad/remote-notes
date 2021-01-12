using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;

namespace RN.WebApi.Presenters.User
{
    public class UpdateUserPresenter : IUpdateUserOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(UpdateUserOutput output) => ViewModel = new OkObjectResult(output.Success);
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
