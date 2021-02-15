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
    public class UserController : ControllerBase
    {

        private readonly ICosmosDbService _cosmosDbService;

        public UserController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [AllowAnonymous]
        [HttpGet("SelectAll")]
        public async Task<IActionResult> SelectAll()
        {
            var result = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
            return Ok(result.ToList());
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> CreateAsync([FromBody] UserModel item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddItemAsync(item);

            return Ok(item);
        }
        
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync([FromBody] UserModel item)
        {
            await _cosmosDbService.UpdateItemAsync(item.Id, item);
            return Ok();
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            UserModel item = await _cosmosDbService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            await _cosmosDbService.DeleteItemAsync(id);

            return Ok();
        }
    }
}
