using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.Note;

namespace RN.WebApi.Presenters.Note
{
    public class GetNotesPresenter : IGetNotesOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(GetNotesOutput output) => ViewModel = new OkObjectResult(new { Notes = output.Notes }); // Change it
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
