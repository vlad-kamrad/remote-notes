using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.Note;

namespace RN.WebApi.Presenters.Note
{
    public class UpdateNotePresenter : IUpdateNoteOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(UpdateNoteOutput output) => ViewModel = new OkObjectResult(output.Success);
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
