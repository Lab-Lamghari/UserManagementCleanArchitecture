using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Auth.Models;

namespace UserManagement.Auth.Manager
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly ICosmosDbService _cosmosDbService;

        private readonly Dictionary<string, string> users = new Dictionary<string, string>
            {{"user01","pass01"},{"user02","pass02"}};
        
        private readonly string key;

        public JwtAuthenticationManager(string key, ICosmosDbService cosmosDbService)
        {
            this.key = key;
            _cosmosDbService = cosmosDbService;

        }
        public async Task<string> Authenticate(string username, string password)
        {

            var items = await _cosmosDbService.GetItemsAsync("SELECT * FROM c where c.UserName = '" + username + "'");

            if (!items.Any(x => x.UserName == username && x.PasswordHash == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
