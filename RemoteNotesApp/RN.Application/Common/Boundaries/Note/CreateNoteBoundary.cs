using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.Note
{
    public sealed class CreateNoteInput
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public CreateNoteInput(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }

    public sealed class CreateNoteOutput
    {
        public int NoteId { get; }
        public CreateNoteOutput(int noteId) => NoteId = noteId;
    }

    public interface ICreateNoteOutputPort : IOutputPortStandard<CreateNoteOutput>, IOutputPortError { }
    public interface ICreateNoteUseCase : IUseCase<CreateNoteInput> { }
}
