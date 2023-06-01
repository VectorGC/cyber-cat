using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public interface IRestClient : IDisposable
    {
        Task<string> PostAsync(string uri, Dictionary<string, string> form, IProgress<float> progress = null);
        Task<string> GetStringAsync(string uri, IProgress<float> progress = null);
        Task<T> GetFromJsonAsync<T>(string uri, IProgress<float> progress = null);
        void AddAuthorizationHeader(string type, string value);
        void RemoveAuthorizationHeader();
    }
}