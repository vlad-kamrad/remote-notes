using AutoMapper;
using RN.Application.Common.Mappings;
using System;

namespace RN.Application.UseCases.User.Querie.GetUserInfo
{
    public class UserInfoVm : IMapFrom<Domain.Entities.User>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.User, UserInfoVm>();
        }
    }
}
