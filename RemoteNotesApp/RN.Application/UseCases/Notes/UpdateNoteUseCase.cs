using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Boundaries.Note;
using RN.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes.Commands
{
    public class UpdateNoteUseCase : IUpdateNoteUseCase
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IUpdateNoteOutputPort outputPort;
        public UpdateNoteUseCase(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            IUpdateNoteOutputPort outputPort)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.outputPort = outputPort;
        }

        public async Task Execute(UpdateNoteInput input)
        {
            var note = await context.Notes.SingleOrDefaultAsync(x => x.Id == input.Id);

            if (note == null)
            {
                outputPort.WriteError("Not Found this note");
                return;
            }

            if (note.UserId != currentUser.UserId)
            {
                outputPort.Standart(new UpdateNoteOutput(false));
                return;
            }

            if (input.Title.Length < 2)
            {
                outputPort.WriteError("Title length must be more than 1 character");
                return;
            }

            if (input.Text.Length > 300)
            {
                outputPort.WriteError("Text length must be less than 300 characters");
                return;
            }

            note.Title = input.Title;
            note.Text = input.Text;

            await context.SaveChangesAsync(new System.Threading.CancellationToken());
            outputPort.Standart(new UpdateNoteOutput(true));
        }
    }
}
