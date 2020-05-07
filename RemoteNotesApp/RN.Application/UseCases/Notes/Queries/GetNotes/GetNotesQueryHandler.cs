using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes.Queries
{
    public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, NotesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IMapper mapper;
        public GetNotesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser, IMapper mapper)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.mapper = mapper;
        }

        public async Task<NotesVm> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            var notes = await context.Notes
                .Where(x => x.UserId == currentUser.UserId)
                .ProjectTo<NoteDto>(mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedDate)  // First new notes
                .ToListAsync();

            return new NotesVm() { Notes = notes };
        }
    }
}
