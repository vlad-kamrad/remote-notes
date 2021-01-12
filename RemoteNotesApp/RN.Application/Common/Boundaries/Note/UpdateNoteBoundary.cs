using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.Note
{
    public sealed class UpdateNoteInput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public UpdateNoteInput(int id, string title, string text)
        {
            Id = id;
            Text = text;
            Title = title;
        }
    }
    public sealed class UpdateNoteOutput
    {
        public bool Success { get; }
        public UpdateNoteOutput(bool success) => Success = success;
    }
    public interface IUpdateNoteOutputPort : IOutputPortStandard<UpdateNoteOutput>, IOutputPortError { }
    public interface IUpdateNoteUseCase : IUseCase<UpdateNoteInput> { }

}
