using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UserManagement.Web.Data
{
    public class UserService
    {
        private readonly ILogger _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task<string> getUserById()
        {
            _logger.LogDebug("Web App : Get User By Id.");

            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri("http://localhost:9320/");

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/User/Get?userID=1");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        result = JsonConvert.DeserializeObject(EmpResponse).ToString();

                        _logger.LogDebug("Web App : Get User By Id Response : " + result);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }            

            return result;
        }
    }
}
