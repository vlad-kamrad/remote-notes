using RN.Application.Common.Boundaries.Interfaces;
using RN.Application.UseCases.Notes.Queries;
using RN.Domain.Common;
using System.Collections.Generic;

namespace RN.Application.Common.Boundaries.Note
{
    public sealed class GetNotesInput { }
    public sealed class GetNotesOutput
    {
        public IList<NoteDto> Notes { get; set; }
        public GetNotesOutput(IList<NoteDto> notes) => Notes = notes;
    }
    public interface IGetNotesOutputPort : IOutputPortStandard<GetNotesOutput>, IOutputPortError { }
    public interface IGetNotesUseCase : IUseCase<GetNotesInput> { }
}
