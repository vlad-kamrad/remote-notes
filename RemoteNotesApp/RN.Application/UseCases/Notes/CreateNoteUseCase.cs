using RN.Application.Common.Boundaries.Note;
using RN.Application.Common.Interfaces;
using RN.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes.Commands
{
    public class CreateNoteUseCase : ICreateNoteUseCase
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly ICreateNoteOutputPort outputPort;

        public CreateNoteUseCase(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            ICreateNoteOutputPort outputPort)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.outputPort = outputPort;
        }

        public async Task Execute(CreateNoteInput input)
        {
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

            var note = new Note
            {
                Title = input.Title,
                Text = input.Text,
                UserId = currentUser.UserId
            };

            context.Notes.Add(note);
            await context.SaveChangesAsync(new CancellationToken());
            outputPort.Standart(new CreateNoteOutput(note.Id));
        }
    }
}
