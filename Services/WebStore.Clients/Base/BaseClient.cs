using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected string Addres { get; set; }

        protected HttpClient Http { get; }

        protected BaseClient(IConfiguration configuration, string serviceAddres)
        {
            Addres = serviceAddres;

            Http = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

        protected T Get<T>(string url) => GetAsync<T>(url).Result; //.GetAwaiter().GetResult();

        protected async Task<T> GetAsync<T>(string url, CancellationToken cancel = default)
        {
            var response = await Http.GetAsync(url, cancel);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancel = default)
        {
            var response = await Http.PostAsJsonAsync(url, item, cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item, CancellationToken cancel = default) => PutAsync(url, item).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancel = default)
        {
            var response = await Http.PutAsJsonAsync(url, item, cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url, CancellationToken cancel = default) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancel = default)
        {
            var response = await Http.DeleteAsync(url,cancel);
            return response.EnsureSuccessStatusCode();
        }

        public void Dispose() => Dispose(true);

        private bool _disposed;
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed) return;
            if(disposing)
            {
                // Очистка управляемых ресурсов
                Http.Dispose();
            }

            // Очистка неуправляемых ресурсов
            _disposed = true;
        }
    }
}
