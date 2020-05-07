using AutoMapper;
using RN.Application.Common.Interfaces;
using RN.Application.Common.Mappings;
using RN.Domain.Entities;
using System;
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
        public List<Role> Roles { get; set; }
        public class UserRoleDto
        {
            public Role Role  { get; set; }
            public Domain.Entities.User User { get; set; }
        }
        public class RoleDto
        {
            public string Name { get; set; }
        }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserRole, Role>()
                .ForMember(x => x.Name, d => d.MapFrom(s => s.Role.Name));
            profile.CreateMap<Domain.Entities.User, UserDto>()
                .ForMember(x => x.Email, d => d.MapFrom(s => s.Email));
        }
    }
}
