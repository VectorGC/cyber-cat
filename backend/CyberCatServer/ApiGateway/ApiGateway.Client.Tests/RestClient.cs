using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApiGateway.Client.Tests;

public class RestClient : IRestClient
{
    private readonly HttpClient _client = new();

    public void Dispose()
    {
        _client?.Dispose();
    }

    public async Task<string> PostAsync(string uri, Dictionary<string, string> form, IProgress<float> progress = null)
    {
        var formData = new MultipartFormDataContent();
        foreach (var (key, value) in form)
        {
            formData.Add(new StringContent(value), key);
        }

        var response = await _client.PostAsync(uri, formData);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetStringAsync(string uri, IProgress<float> progress = null)
    {
        return await _client.GetStringAsync(uri);
    }

    public async Task<T> GetFromJsonAsync<T>(string uri, IProgress<float> progress = null)
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
}