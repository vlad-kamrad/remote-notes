using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Interfaces;
using RN.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IApplicationDbContext context;
        public TokenService(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddRefreshTokenAsync(RefreshToken token)
        {
            try
            {
                await context.RefreshTokens.AddAsync(token);
                await context.SaveChangesAsync(new CancellationToken());
                return true;
            }
            catch
            {
                // TODO: Use custm exc
                return false;
            }
        }

        public async Task<RefreshToken> FindRefreshTokenAsync(string token, string userId)
        {
            var refreshToken = await context.RefreshTokens
                .FirstOrDefaultAsync(s => s.Token == token && s.UserId == userId);

            return refreshToken;
        }

        public async Task<RefreshToken> FindRefreshTokenAsync(string userId)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<bool> RemoveRefreshTokenAsync(string userId)
        {
            try
            {
                var token = await context.RefreshTokens.FirstOrDefaultAsync(s => s.UserId == userId);
                if (token != null)
                {
                    context.RefreshTokens.Remove(token);
                    await context.SaveChangesAsync(new CancellationToken());
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
