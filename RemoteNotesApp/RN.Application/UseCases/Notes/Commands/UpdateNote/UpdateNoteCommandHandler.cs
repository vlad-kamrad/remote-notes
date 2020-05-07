using MediatR;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        public UpdateNoteCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }

        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
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

            note.Title = request.Title;
            note.Text = request.Text;

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
