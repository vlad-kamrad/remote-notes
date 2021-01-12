using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Boundaries.Note;
using RN.Application.Common.Interfaces;
using RN.Application.UseCases.Notes.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace RN.Application.UseCases.Notes
{
    public class GetNotesUseCase : IGetNotesUseCase
    {
        private readonly IApplicationDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IGetNotesOutputPort outputPort;
        private readonly IMapper mapper;
        public GetNotesUseCase(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            IGetNotesOutputPort outputPort,
            IMapper mapper)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.outputPort = outputPort;
            this.mapper = mapper;
        }

        public async Task Execute(GetNotesInput input)
        {
            var notes = await context.Notes
                .Where(x => x.UserId == currentUser.UserId)
                .ProjectTo<NoteDto>(mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedDate)  // First new notes
                .ToListAsync();

            outputPort.Standart(new GetNotesOutput(notes));
        }
    }
}
