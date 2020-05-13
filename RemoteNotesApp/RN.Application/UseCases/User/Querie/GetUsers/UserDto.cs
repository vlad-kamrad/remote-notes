using AutoMapper;
using RN.Application.Common.Mappings;
using RN.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RN.Application.UseCases.User.Querie.GetUsers
{
    public class UserDto : IMapFrom<Domain.Entities.User>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRole, Role>()
                .ForMember(x => x.Name, d => d.MapFrom(s => s.Role.Name));
            profile.CreateMap<Domain.Entities.User, UserDto>()
                .ForMember(x => x.Roles, d => d.MapFrom(s => s.Roles.Select(e => e.Role.Name)));
        }
    }
}
