using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.Dto;
using Shared.Dto.Args;

namespace ApiGateway.Tests.End2End.Extensions;

public class TestUserHttpClient : HttpClient, IAsyncDisposable
{
    public const string TestEmail = "test_user_name@test.com";
    public const string TestUserPassword = "TestUserPassword123456@";
    public const string TestUserName = "TestUserName";

    public static async Task<TestUserHttpClient> Create()
    {
        var client = new TestUserHttpClient();
        await client.AddTestUser();
        await client.AddAuthHeader();

        return client;
    }

    private TestUserHttpClient()
    {
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            return;
        }

        throw new InvalidOperationException("Use DisposeAsync instead");
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveTestUser();
        base.Dispose();
    }

    public async Task AddAuthHeader()
    {
        var token = await GetTokenForTestUser(this);
        this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
    }

    public static async Task<string> GetTokenForTestUser(HttpClient client)
    {
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(TestEmail), "email");
        form.Add(new StringContent(TestUserPassword), "password");

        var response = await client.PostAsync("http://localhost:5000/auth/login", form);
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

        var response = await this.PostAsJsonAsync("http://localhost:5000/auth/create", args);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveTestUser()
    {
        var email = TestEmail;
        var response = await this.PostAsJsonAsync("http://localhost:5000/auth/remove", email);
        response.EnsureSuccessStatusCode();
    }
}