using System.Collections.Generic;
using UserManagement.Application.User.DTO;

namespace UserManagement.Application.User.VM
{
    public class UserVM
    {
        public IList<UserDTO> UserList { get; set; }
    }
}