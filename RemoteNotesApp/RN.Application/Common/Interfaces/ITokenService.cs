using RN.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RN.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<RefreshToken> FindRefreshTokenAsync(string token, string userId);
        Task<RefreshToken> FindRefreshTokenAsync(string userId);
        Task<bool> RemoveRefreshTokenAsync(string userId);
        Task<bool> AddRefreshTokenAsync(RefreshToken token);
    }
}
