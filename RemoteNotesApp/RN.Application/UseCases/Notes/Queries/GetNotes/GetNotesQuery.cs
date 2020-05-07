using MediatR;

namespace RN.Application.UseCases.Notes.Queries
{
    public class GetNotesQuery : IRequest<NotesVm> { }
}
