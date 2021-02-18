using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Auth.Models;

namespace UserManagement.Auth.Manager
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(UserModel item)
        {
            try
            {
                await this._container.CreateItemAsync<UserModel>(item, new PartitionKey(item.UserName));

            }
            catch (Exception ex)
            {

                var res = ex.Message;
            }        
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<UserModel>(id, new PartitionKey(id));
        }

        public async Task<UserModel> GetItemAsync(string UserName)
        {
            try
            {
                ItemResponse<UserModel> response = await this._container.ReadItemAsync<UserModel>(UserName, new PartitionKey(UserName));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<UserModel>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<UserModel>(new QueryDefinition(queryString));
            List<UserModel> results = new List<UserModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, UserModel item)
        {
            await this._container.UpsertItemAsync<UserModel>(item, new PartitionKey(id));
        }
    }
}
