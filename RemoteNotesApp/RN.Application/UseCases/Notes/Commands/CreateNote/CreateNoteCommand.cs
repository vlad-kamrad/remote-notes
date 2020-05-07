using MediatR;

namespace RN.Application.UseCases.Notes.Commands.CreateNote
{
    public class CreateNoteCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
