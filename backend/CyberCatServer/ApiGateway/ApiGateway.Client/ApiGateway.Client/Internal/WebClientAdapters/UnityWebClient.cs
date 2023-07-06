using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ApiGateway.Client
{
    internal class UnityWebClient : IWebClient
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void AddAuthorizationHeader(string type, string value)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAuthorizationHeader()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            using (var request = UnityWebRequest.Post(uri, form))
            {
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();

                return request.downloadHandler.text;
            }
        }

        public async Task<string> GetStringAsync(string uri)
        {
            using (var request = UnityWebRequest.Get(uri))
            {
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();

                return request.downloadHandler.text;
            }
        }

        public Task<T> GetFromJsonAsync<T>(string uri)
        {
            throw new System.NotImplementedException();
        }

        public Task PostStringAsync(string uri, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task PostAsJsonAsync<TValue>(string uri, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponse> PostAsJsonAsync<TResponse>(string uri, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(string uri)
        {
            throw new System.NotImplementedException();
        }
    }
}