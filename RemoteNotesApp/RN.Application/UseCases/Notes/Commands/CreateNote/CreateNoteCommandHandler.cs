using MediatR;
using RN.Application.Common.Interfaces;
using RN.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        public CreateNoteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }

        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                Title = request.Title,
                Text = request.Text,
                UserId = currentUser.UserId
            };

            context.Notes.Add(note);
            await context.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
