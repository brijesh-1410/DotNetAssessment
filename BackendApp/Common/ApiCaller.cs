using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BackendApp.Common
{
    
    public class ApiCaller : IApiCaller
    {
        private readonly string bapiURL;
        //private IConfiguration configuration;
         public ApiCaller(IConfiguration configuration) {
            bapiURL = configuration.GetValue<string>("Dev:BAPIUrl");
        }

        public async Task<string> Get(string URL) {
            
            string finalURL = bapiURL + URL;
            string responseBody = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(finalURL);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException while fetching data from Business API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public async Task<bool> Post(string URL, JObject inputObject) {
            string finalURL = bapiURL + URL;
            string responseBody = string.Empty;
            inputObject.Remove("rowKey");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(inputObject);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(finalURL, stringContent);

                    response.EnsureSuccessStatusCode();
                    //responseBody = await response.Content.ReadAsStringAsync();
                    return response.IsSuccessStatusCode;
                }
                return false;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException while fetching data from Business API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public async Task<bool> Delete(string URL)
        {

            string finalURL = bapiURL + URL;
            string responseBody = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync(finalURL);
                    response.EnsureSuccessStatusCode();
                    //responseBody = await response.Content.ReadAsStringAsync();
                    return response.IsSuccessStatusCode;
                }
                return false;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException while fetching data from Business API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }
    }
}
