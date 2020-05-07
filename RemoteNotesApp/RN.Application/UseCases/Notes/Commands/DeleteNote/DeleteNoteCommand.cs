using MediatR;

namespace RN.Application.UseCases.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
