using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;

namespace RN.Application.Common.Boundaries.Note
{
    public sealed class DeleteNoteInput
    {
        public int Id { get; set; }
        public DeleteNoteInput(int id) => Id = id;

    }
    public sealed class DeleteNoteOutput
    {
        public bool Success { get; }
        public DeleteNoteOutput(bool success) => Success = success;
    }
    public interface IDeleteNoteOutputPort : IOutputPortStandard<DeleteNoteOutput>, IOutputPortError { }
    public interface IDeleteNoteUseCase : IUseCase<DeleteNoteInput> { }

}
