using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RN.WebApi.DTO
{
    public class ChangeRoleDto
    {
        public string UserId { get; set; }
        public List<string> DesiredRoles { get; set; }
    }

    public class InputUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class LoginParams
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UpdateAccessTokenDto
    {
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
    }
}
