using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Auth.Manager
{
    public interface IJwtAuthenticationManager
    {
        Task<string> Authenticate(string username, string password);
    }
}
