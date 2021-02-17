using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Web.Data
{
    public class AuthService
    {
        public async Task<String> Authenticate(string userName, string password)
        {
            string Token = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("username", userName);
            dic.Add("password", password);

            try
            {
                using (var client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(dic);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    client.BaseAddress = new Uri("http://localhost:5910/");

                    client.DefaultRequestHeaders.Clear();
                    
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    HttpResponseMessage Res = await client.PostAsync("api/Authentication/authenticate", content);
                    
                    if (Res.IsSuccessStatusCode)
                    {                        
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;                        
                        Token = JsonConvert.DeserializeObject(EmpResponse).ToString();                        

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Token;
        }
    }
}
