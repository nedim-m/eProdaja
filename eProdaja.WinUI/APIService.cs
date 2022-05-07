using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eProdaja.Model;

namespace eProdaja.WinUI
{
    public class APIService
    {
        private string _resourceName = null;
        public string _endPoint = "http://localhost:5095/";
        public APIService(string resourceName)
        {
            _resourceName=resourceName;
        }



        public async Task<T> Get<T>(object search = null)
        {
           
            var query = "";

            if (search!=null)
            {
                

                query= await search.ToQueryString();
            }
            var list = await $"{_endPoint}{_resourceName}?{query}".GetJsonAsync<T>();

            

            return list;
        }

        public async Task<T> GetById<T>(object id)
        {
            var result = await $"{_endPoint}{_resourceName}/{id}".GetJsonAsync<T>();

            return result;
        }

        public async Task<T> Post<T>(object request)
        {
            var result = await $"{_endPoint}{_resourceName}".PostJsonAsync(request).ReceiveJson<T>();

            return result;
        }


        public async Task<T> Put<T>(object id, object request)
        {
            var result = await $"{_endPoint}{_resourceName}/{id}".PutJsonAsync(request).ReceiveJson<T>();

            return result;
        }
    }
}
