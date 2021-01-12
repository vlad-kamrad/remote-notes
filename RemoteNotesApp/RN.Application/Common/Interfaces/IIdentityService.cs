using RN.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RN.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User updatedUser);
        Task<bool> CheckPassword(string userId, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<User> FindByIdAsync(string userId);
        Task<List<Role>> GetUserRoles(string userId);
        Task<bool> AddRole(string userId, string roleName);
        Task<bool> RemoveRole(string userId, string roleName);
        Task<bool> CheckRole(string userId, string roleName);
        Task GetUserRoles(object userId);
        // TODO: implement user deletion & updating
    }
}
