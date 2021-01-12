using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.User;

namespace RN.WebApi.Presenters.User
{
    public class GetUserInfoPresenter : IGetUserInfoOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(GetUserInfoOutput output) => ViewModel = new OkObjectResult(new
        {
            Id = output.Id,
            Name = output.Name,
            Surname = output.Surname,
            Email = output.Email,
            Created = output.Created,
            Modified = output.Modified
        });
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
