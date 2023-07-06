using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Client.Internal.WebClientAdapters
{
    internal interface IWebClient : IDisposable
    {
        void AddAuthorizationHeader(string type, string value);
        void RemoveAuthorizationHeader();
        Task<string> PostAsync(string uri, Dictionary<string, string> form);
        Task<string> GetStringAsync(string uri);
        Task<T> GetFromJsonAsync<T>(string uri);
        Task PostStringAsync(string uri, string value);
        Task PostAsJsonAsync<TValue>(string uri, TValue value);
        Task<TResponse> PostAsJsonAsync<TResponse>(string uri, string value);
        Task DeleteAsync(string uri);
    }
}