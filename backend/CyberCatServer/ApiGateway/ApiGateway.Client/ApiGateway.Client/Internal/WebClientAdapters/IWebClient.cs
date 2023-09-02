using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ApiGateway.Client.Internal.WebClientAdapters
{
    internal interface IWebClient : IDisposable
    {
        void AddAuthorizationHeader(string type, string value);
        Task<string> GetStringAsync(string uri);
        Task<T> GetFromJsonAsync<T>(string uri);
        Task<string> PostAsync(string uri, NameValueCollection form);
        Task<string> PostAsync(string uri);
        Task<TResponse> PostAsJsonAsync<TResponse>(string uri, NameValueCollection form);
        Task<string> PutAsync(string uri, NameValueCollection form);
        Task DeleteAsync(string uri);
        Task DeleteAsync(string uri, NameValueCollection form);
    }
}