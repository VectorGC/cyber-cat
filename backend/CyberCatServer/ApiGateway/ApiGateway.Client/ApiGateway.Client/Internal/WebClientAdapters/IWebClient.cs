using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Client.Internal.WebClientAdapters
{
    internal interface IWebClient : IDisposable
    {
        void AddAuthorizationHeader(string type, string value);
        Task<string> GetStringAsync(string uri);
        Task<T> GetFromJsonAsync<T>(string uri);
        Task<string> PostAsync(string uri, Dictionary<string, string> form);
        Task<TResponse> PostAsJsonAsync<TResponse>(string uri, Dictionary<string, string> form);
        Task DeleteAsync(string uri);
    }
}