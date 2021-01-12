using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.Note;

namespace RN.WebApi.Presenters.Note
{
    public class CreateNotePresenter : ICreateNoteOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Standart(CreateNoteOutput output) => ViewModel = new OkObjectResult(output.NoteId);
        public void WriteError(string message) => ViewModel = new BadRequestObjectResult(message);
    }
}
