using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Auth.Models;

namespace UserManagement.Auth.Manager
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<UserModel>> GetItemsAsync(string query);
        Task<UserModel> GetItemAsync(string id);
        Task AddItemAsync(UserModel item);
        Task UpdateItemAsync(string id, UserModel item);
        Task DeleteItemAsync(string id);
    }
}
