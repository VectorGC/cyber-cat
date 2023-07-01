using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ApiGateway.Client.Tests.RestClient
{
    public class HttpRestClientAdapter : IRestClient
    {
        private readonly HttpClient _client = new HttpClient();

        public void Dispose()
        {
            _client?.Dispose();
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            var formData = new MultipartFormDataContent();
            foreach (var kvp in form)
            {
                formData.Add(new StringContent(kvp.Value), kvp.Key);
            }

            var response = await _client.PostAsync(uri, formData);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetStringAsync(string uri)
        {
            return await _client.GetStringAsync(uri);
        }

        public async Task<T> GetFromJsonAsync<T>(string uri)
        {
            return await _client.GetFromJsonAsync<T>(uri);
        }

        public void AddAuthorizationHeader(string type, string value)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }

        public void RemoveAuthorizationHeader()
        {
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task PostStringAsync(string uri, string value)
        {
            var response = await _client.PostAsJsonAsync(uri, value);
            response.EnsureSuccessStatusCode();
        }

        public async Task PostAsJsonAsync<TValue>(string uri, TValue value)
        {
            var response = await _client.PostAsJsonAsync(uri, value);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, string value)
        {
            var response = await _client.PostAsJsonAsync(uri, value);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task DeleteAsync(string uri)
        {
            var response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}