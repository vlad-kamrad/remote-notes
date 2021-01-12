using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Boundaries.Note;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes
{
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IDeleteNoteOutputPort outputPort;

        public DeleteNoteUseCase(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            IDeleteNoteOutputPort outputPort)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.outputPort = outputPort;
        }
        public async Task Execute(DeleteNoteInput input)
        {
            var note = await context.Notes.SingleOrDefaultAsync(x => x.Id == input.Id);

            if (note == null)
            {
                outputPort.WriteError("Not Found this note");
                return;
            }

            if (note.UserId != currentUser.UserId)
            {
                outputPort.Standart(new DeleteNoteOutput(false));
                return;
            }

            context.Notes.Remove(note);
            await context.SaveChangesAsync(new System.Threading.CancellationToken());

            outputPort.Standart(new DeleteNoteOutput(true));
        }
    }
}
