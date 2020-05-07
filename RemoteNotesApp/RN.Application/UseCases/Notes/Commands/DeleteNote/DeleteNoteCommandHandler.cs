using MediatR;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;

        public DeleteNoteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }
        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await context.Notes.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (note == null)
            {
                throw new NotFoundException();
            }

            if (note.UserId != currentUser.UserId)
            {
                throw new BadRequestException();
            }

            context.Notes.Remove(note);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
