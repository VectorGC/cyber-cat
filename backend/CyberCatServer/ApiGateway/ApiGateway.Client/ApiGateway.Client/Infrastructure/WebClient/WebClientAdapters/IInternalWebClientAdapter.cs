using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Client.Infrastructure.WebClient.WebClientAdapters
{
    internal interface IInternalWebClientAdapter : IDisposable
    {
        void AddAuthorizationHeader(string type, string value);
        void RemoveAuthorizationHeader();
        Task<string> GetStringAsync(string uri);
        Task<TResponse> GetFromJsonAsync<TResponse>(string uri);
        Task<TResponse> GetFromFastJsonPolymorphicAsync<TResponse>(string uri);
        Task<string> PostAsync(string uri, Dictionary<string, string> form);
        Task<string> PostAsync(string uri);
        Task<TResponse> PostAsJsonAsync<TResponse>(string uri, Dictionary<string, string> form);
        Task<TResponse> PostAsFastJsonPolymorphicAsync<TResponse>(string uri, Dictionary<string, string> form);
        void AddHeader(string header, string value);
        void RemoveHeader(string header);
    }
}