using RN.Domain.Entities;
using System.Collections.Generic;

namespace RN.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
