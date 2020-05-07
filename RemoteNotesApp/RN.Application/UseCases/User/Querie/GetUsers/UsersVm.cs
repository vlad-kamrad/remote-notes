using System.Collections.Generic;

namespace RN.Application.UseCases.User.Querie.GetUsers
{
    public class UsersVm
    {
        public IList<UserDto> Users { get; set; }
    }
}
