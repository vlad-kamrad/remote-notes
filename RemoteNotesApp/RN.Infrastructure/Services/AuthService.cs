using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using RN.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RN.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityService identityService;
        private readonly ITokenService tokenService;
        public AuthService(IIdentityService identityService, ITokenService tokenService)
        {
            this.identityService = identityService;
            this.tokenService = tokenService;
        }

        public async Task<string> GenerateAccessToken(string userName, string password)
        {
            string userId = await identityService.GetUserIdAsync(userName);

            // TODO: Use Exception
            if (!await identityService.CheckPassword(userId, password)) { return null; }

            return await CombinateResult(userName, userId);
        }

        // Rename this
        public async Task<string> GenerateRefrershToken(string refreshToken, string userName)
        {
            var userId = await identityService.GetUserIdAsync(userName);
            var token = await tokenService.FindRefreshTokenAsync(refreshToken, userId);

            if (token == null) 
                throw new BadRequestException();

            if (token.ExpiresUtc < DateTime.UtcNow.ToUniversalTime())
                throw new UnauthorizedExceptions();

            //var userName = await identityService.GetUserNameAsync(userId);
            return await CombinateResult(userName, userId);
        }

        // Set params
        private async Task<SecurityTokenDescriptor> GetSecurityTokenDescriptor(string userId)
        {
            return new SecurityTokenDescriptor
            {
                Subject = await CreateClaimsIdentity(userId),
                /*new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, userId),
                    //    new Claim(ClaimTypes.Role, "user")
                }),*/
                Audience = "123",
                Issuer = "123",
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("keyaisjdoasijdioasjdoiasjdoisajd")), SecurityAlgorithms.HmacSha256)
            };
        }

        private async Task<ClaimsIdentity> CreateClaimsIdentity(string userId)
        {
            var claimsRoles = await identityService.GetUserRoles(userId);

                var a = claimsRoles.Select(x => new Claim(ClaimTypes.Role, x.Name));

                var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, userId) };
                claims.AddRange(a);

            //return new ClaimsIdentity(claims);
            return new ClaimsIdentity(claims);
        }

        // Add or replace refresh token, and save in DB
        private async Task<RefreshToken> GetRefreshToken(string userId)
        {
            var refreshToken = await tokenService.FindRefreshTokenAsync(userId);

            if (refreshToken != null)
            {
                await tokenService.RemoveRefreshTokenAsync(userId);
            }

            // TODO: Use corect path
            var newRefreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(), // TODO: Use special method for creating tokens
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            };

            await tokenService.AddRefreshTokenAsync(newRefreshToken);
            return newRefreshToken;
        }

        // Combinate result with access/refresh tokens, user id/name and expiration time
        private string GenerateTokenResponse(
            JwtSecurityTokenHandler tokenHandler,
            SecurityToken token,
            RefreshToken newRefreshToken,
            string userName,
            List<string> roles)
        {
            // TODO: use const seconds
            return JsonConvert.SerializeObject(new SerializeTokenObject
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = newRefreshToken.Token,
                TokenExpiration = DateTime.UtcNow.AddSeconds(1440),
                UserName = userName, 
                Roles = roles
            });
        }

        // Invoke all helper functions and transform to JSON 
        private async Task<string> CombinateResult(string userName, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = await GetSecurityTokenDescriptor(userId);
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            var newRefreshToken = await GetRefreshToken(userId);
            var userRoles = await identityService.GetUserRoles(userId);
            var a = userRoles.Select(x => x.Name).ToList();

            return GenerateTokenResponse(tokenHandler, token, newRefreshToken, userName, a);
        }

        private class SerializeTokenObject
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public DateTime TokenExpiration { get; set; }
            public string UserName { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}
