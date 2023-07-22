using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApp.Common
{
    public interface IApiCaller
    {
        public Task<string> Get(string URL);
        public Task<bool> Post(string URL,JObject inputObject);
        public Task<bool> Delete(string URL);

        //public Task<string> Get(string URL,int id);
    }
}
