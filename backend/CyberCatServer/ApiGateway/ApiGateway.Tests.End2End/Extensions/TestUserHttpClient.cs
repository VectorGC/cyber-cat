using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Shared.Dto;
using Shared.Dto.Args;

namespace ApiGateway.Tests.End2End.Extensions;

public class TestUserHttpClient : IAsyncDisposable
{
    public const string TestEmail = "test_user_name@test.com";
    public const string TestUserPassword = "TestUserPassword123456@";
    public const string TestUserName = "TestUserName";

    private const string Host = ApiGatewayTestSettings.URL;

    private readonly HttpClient _client;

    public static async Task<TestUserHttpClient> Create()
    {
        var client = new TestUserHttpClient();
        await client.AddTestUser();
        await client.AddAuthHeader();

        return client;
    }

    private TestUserHttpClient()
    {
        _client = new HttpClient();
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveTestUser();
        _client.Dispose();
    }

    public async Task AddAuthHeader()
    {
        var token = await GetTokenForTestUser();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
    }

    public async Task<string> GetTokenForTestUser()
    {
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(TestEmail), "email");
        form.Add(new StringContent(TestUserPassword), "password");

        var response = await PostAsync("/auth/login", form);
        var token = await response.Content.ReadFromJsonAsync<TokenDto>();

        return token.AccessToken;
    }

    public async Task AddTestUser()
    {
        var email = TestEmail;
        var username = TestUserName;
        var args = new CreateUserArgs()
        {
            User = new UserDto()
            {
                UserName = TestUserName,
                Email = TestEmail
            },
            Password = TestUserPassword
        };

        var response = await PostAsJsonAsync("/auth/create", args);
        response.EnsureSuccessStatusCode();
    }

    private async Task RemoveTestUser()
    {
        var email = TestEmail;
        var response = await PostAsJsonAsync("/auth/remove", email);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> GetStringAsync(string endpoint)
    {
        return await _client.GetStringAsync(Host + endpoint);
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync(string endpoint, string? value)
    {
        return await _client.PostAsJsonAsync(Host + endpoint, value);
    }

    public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string? endpoint, TValue value)
    {
        return await _client.PostAsJsonAsync(Host + endpoint, value);
    }

    public async Task<HttpResponseMessage> PostAsync(string? endpoint, HttpContent? content)
    {
        return await _client.PostAsync(Host + endpoint, content);
    }

    public async Task<TValue?> GetFromJsonAsync<TValue>(string? endpoint)
    {
        return await _client.GetFromJsonAsync<TValue>(Host + endpoint);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string? endpoint)
    {
        return await _client.DeleteAsync(Host + endpoint);
    }

    public async Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string? endpoint, TValue value)
    {
        return await _client.PutAsJsonAsync(Host + endpoint, value);
    }
}