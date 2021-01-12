using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;

namespace RN.WebApi.Presenters.User
{
    public class UpdateAccessTokenPresenter : IUpdateAccessTokenOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(UpdateAccessTokenOutput output) => ViewModel = new OkObjectResult(output.AccessToken);
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
