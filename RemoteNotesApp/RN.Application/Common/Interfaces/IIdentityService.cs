using RN.Application.Common.Models;
using RN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RN.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<bool> CreateUserAsync(User user); // TODO: maybe email address
        Task<bool> CheckPassword(string userId, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<User> FindByIdAsync(string userId);
        Task<List<Role>> GetUserRoles(string userId);
        Task<bool> AddRole(string userId, string roleName);
        Task<bool> RemoveRole(string userId, string roleName);
        Task<bool> CheckRole(string userId, string roleName);
        // TODO: implement user deletion & updating
    }
}
