using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Auth.Models;

namespace UserManagement.Auth.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        public string Get()
        {
            return "Hello!";
        }

        public IActionResult Authenticate([FromBody] UserCredentials userCred)
        {
            return Ok();
        }
    }
}
