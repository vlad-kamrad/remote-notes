using AutoMapper;
using RN.Application.Common.Mappings;
using RN.Domain.Entities;

namespace RN.Application.UseCases.User.Querie.GetRoles
{
    public class RoleDto : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, RoleDto>();
        }
    }
}
