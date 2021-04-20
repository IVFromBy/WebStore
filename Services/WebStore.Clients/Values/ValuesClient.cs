using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration, "api/values") {}

        public Uri Create(string value)
        {
            var response = Http.PostAsJsonAsync(Addres, value).Result;

            return response.EnsureSuccessStatusCode().Headers.Location;
            
        }

        public HttpStatusCode Edit(int id, string values)
        {
            var response = Http.PutAsJsonAsync($"{Addres}/{id}", values).Result;

            return response.EnsureSuccessStatusCode().StatusCode;
        }

        public IEnumerable<string> Get()
        {
            var response = Http.GetAsync(Addres).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var response = Http.GetAsync($"{Addres}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<string>().Result;

            return string.Empty;
        }

        public bool Remove(int id)
        {
            var response = Http.DeleteAsync($"{Addres}/{id}").Result;

            return response.IsSuccessStatusCode;
        }
    }
}
