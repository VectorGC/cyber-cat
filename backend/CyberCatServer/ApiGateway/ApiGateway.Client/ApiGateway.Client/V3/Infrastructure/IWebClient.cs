using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Client.V3.Infrastructure
{
    internal interface IWebClient : IDisposable
    {
        Task<string> GetAsync(string path);
        Task<TResponse> GetAsync<TResponse>(string path);
        Task<TResponse> GetFastJsonAsync<TResponse>(string path);
        Task<string> PostAsync(string path, Dictionary<string, string> form);
        Task<string> PostAsync(string path);
        Task<TResponse> PostAsync<TResponse>(string path, Dictionary<string, string> form);
        Task<TResponse> PostFastJsonAsync<TResponse>(string path, Dictionary<string, string> form);
    }
}