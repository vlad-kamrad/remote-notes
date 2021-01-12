using Microsoft.EntityFrameworkCore;
using RN.Application.Common.Exceptions;
using RN.Application.Common.Interfaces;
using RN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationDbContext context;
        private readonly IPasswordHasher passwordHasher;
        public IdentityService(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<bool> CheckPassword(string userId, string password)
        {
            var user = await FindByIdAsync(userId);
            return passwordHasher.ValidatePassword(password, user.Password); // TODO: Change to async
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            user.Password = passwordHasher.EncodePassword(user.Password);
            user.Id = Guid.NewGuid().ToString();
            user.Created = DateTime.Now;

            await context.Users.AddAsync(user);

            // TODO: Change it
            var defaultRole = await context.Roles.FirstOrDefaultAsync(x => x.Id == 1);

            await context.UserRoles.AddAsync(new UserRole
            {
                User = user,
                Role = defaultRole
            });

            await context.SaveChangesAsync(new CancellationToken());

            return true;
        }

        public async Task<bool> UpdateUserAsync(User updatedUser)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == updatedUser.Id);
            if (user == null) return false;

            var checkName = await context.Users.SingleOrDefaultAsync(x => x.Name == updatedUser.Name);
            if (checkName != null) return false;

            if (updatedUser.Name != null) user.Name = updatedUser.Name;
            if (updatedUser.Surname != null) user.Surname = updatedUser.Surname;
            if (updatedUser.Email != null) user.Email = updatedUser.Email;
            user.LastModified = DateTime.Now;
            if (updatedUser.Password != null) {
                user.Password = passwordHasher.EncodePassword(updatedUser.Password);
            }

            context.SetModifiedState(user);
            await context.SaveChangesAsync(new CancellationToken());

            return true;
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await FindByNameAsync(userName);
            return user.Id;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await FindByIdAsync(userId);
            return user.Name;
        }

        public async Task<List<Role>> GetUserRoles(string userId)
        {
            var roleIds = context.UserRoles.Where(x => x.User.Id == userId).Select(x => x.Role.Id);
            return await context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return user;
        }

        public async Task<bool> AddRole(string userId, string roleName)
        {
            User user = await FindByIdAsync(userId);

            var isCurrentRole = await context.UserRoles
                .SingleOrDefaultAsync(x => x.User.Id == userId && x.Role.Name == roleName);

            if (isCurrentRole != null) return false;

            var role = await context.Roles.SingleOrDefaultAsync(x => x.Name == roleName);

            context.UserRoles.Add(new UserRole
            {
                User = user,
                Role = role
            });

            await context.SaveChangesAsync(new CancellationToken());

            return true;
        }

        public async Task<bool> RemoveRole(string userId, string roleName)
        {
            try
            {
                var userRole = await context.UserRoles
                .SingleOrDefaultAsync(x => x.User.Id == userId && x.Role.Name == roleName);

                context.UserRoles.Remove(userRole);
                await context.SaveChangesAsync(new CancellationToken());
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckRole(string userId, string roleName)
        {
            var roles = await GetUserRoles(userId);
            return roles.Select(x => x.Name).Contains(roleName);
        }

        private async Task<User> FindByNameAsync(string userName)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Name == userName);
            if (user == null)
            {
                throw new NotFoundException($"User {userName} not found");
            }

            return user;
        }

        public Task GetUserRoles(object userId)
        {
            throw new NotImplementedException();
        }
    }
}
