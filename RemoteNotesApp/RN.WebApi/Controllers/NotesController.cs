using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RN.Application.UseCases.Notes.Commands.CreateNote;
using RN.Application.UseCases.Notes.Commands.DeleteNote;
using RN.Application.UseCases.Notes.Commands.UpdateNote;
using RN.Application.UseCases.Notes.Queries;
using System.Threading.Tasks;

namespace RN.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<NotesVm> GetNotes() =>
            await Mediator.Send(new GetNotesQuery());

        [HttpGet("{id}")]
        public async Task<string> GetNote(int id) => "";

        [Authorize]
        [HttpPost]
        public async Task<int> Create([FromBody] CreateNoteCommand createNoteCommand) =>
            await Mediator.Send(createNoteCommand);

        [Authorize]
        [HttpPut]
        public async Task<bool> Update([FromBody] UpdateNoteCommand updateNoteCommand) =>
            await Mediator.Send(updateNoteCommand);

        [Authorize]
        [HttpDelete]
        public async Task<bool> Delete([FromBody] DeleteNoteCommand deleteNoteCommand) =>
            await Mediator.Send(deleteNoteCommand);

    }
}
