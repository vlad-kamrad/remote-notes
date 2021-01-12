using RN.Application.Common.Boundaries.Interfaces;
using RN.Domain.Common;
using System;

namespace RN.Application.Common.Boundaries.User
{
    public sealed class GetUserInfoInput { }
    public sealed class GetUserInfoOutput
    {
        public string Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public DateTime Created { get; }
        public DateTime? Modified { get; }

        public GetUserInfoOutput(
            string id,
            string name,
            string surname,
            string email,
            DateTime created,
            DateTime? modified)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Created = created;
            Modified = modified;
        }
    }
    public interface IGetUserInfoOutputPort : IOutputPortStandard<GetUserInfoOutput>, IOutputPortError { }
    public interface IGetUserInfoUseCase : IUseCase<GetUserInfoInput> { }
}
