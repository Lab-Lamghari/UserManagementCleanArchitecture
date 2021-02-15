using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Auth.Manager;
using UserManagement.Auth.Models;

namespace UserManagement.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationManager JwtAuthenticationManager;

        public AuthenticationController(IJwtAuthenticationManager JwtAuthenticationManager)
        {
            this.JwtAuthenticationManager = JwtAuthenticationManager;
        }
        
        [AllowAnonymous]
        public string Get()
        {
            return "Hello!";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCred)
        {
            var result = JwtAuthenticationManager.Authenticate(userCred.username, userCred.password);
            if (string.IsNullOrEmpty(result))
                return Unauthorized();
            return Ok(result);
        }
    }
}
