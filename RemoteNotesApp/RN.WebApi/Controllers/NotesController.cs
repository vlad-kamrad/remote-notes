using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RN.Application.Common.Boundaries.Note;
using RN.WebApi.DTO;
using RN.WebApi.Presenters.Note;
using System.Threading.Tasks;

namespace RN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetNotes(
            [FromServices] IGetNotesUseCase useCase,
            [FromServices] GetNotesPresenter presenter)
        {
            await useCase.Execute(new GetNotesInput());
            return presenter.ViewModel;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICreateNoteUseCase useCase,
            [FromServices] CreateNotePresenter presenter,
            [FromBody] CreateNoteDto _)
        {
            await useCase.Execute(new CreateNoteInput(_.Title, _.Text));
            return presenter.ViewModel;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateNoteUseCase useCase,
            [FromServices] UpdateNotePresenter presenter,
            [FromBody] UpdateNoteDto input)
        {
            await useCase.Execute(new UpdateNoteInput(input.Id, input.Title, input.Text));
            return presenter.ViewModel;
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteNoteUseCase useCase,
            [FromServices] DeleteNotePresenter presenter,
            [FromBody] NoteId input)
        {
            await useCase.Execute(new DeleteNoteInput(input.Id));
            return presenter.ViewModel;
        }

    }
}
