using AutoMapper;
using RN.Application.Common.Mappings;
using RN.Domain.Entities;
using System;

namespace RN.Application.UseCases.Notes.Queries
{
    public class NoteDto : IMapFrom<Note>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteDto>()
                .ForMember(d => d.UserName, x => x.MapFrom(s => s.User != null ? s.User.Name : string.Empty))
                .ForMember(d => d.CreatedDate, x => x.MapFrom(s => s.Created))
                .ForMember(d => d.ModifiedDate, x => x.MapFrom(s => s.LastModified));
        }
    }
}
