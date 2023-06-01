using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Dto;
using Shared.Dto.Args;

namespace ApiGateway.Client.Tests.Abstracts;

[TestFixture]
public abstract class ClientTests
{
    private const string Host = ApiGatewayTestSettings.URL;
    protected Client Client;

    protected const string TestEmail = "test_user_name@test.com";
    protected const string TestUserPassword = "TestUserPassword123456@";
    protected const string TestUserName = "TestUserName";

    private HttpClient _httpClient;

    [SetUp]
    public async Task SetUp()
    {
        var restClient = new RestClient();
        Client = new Client(Host, restClient);

        _httpClient = new HttpClient();
        await AddTestUser();

        var token = await GetTokenForTestUser();
        Client.AddAuthorizationToken(token);
    }

    [TearDown]
    public async Task TearDown()
    {
        await AddAuthHeader();
        await RemoveTestUser();

        Client.Dispose();
        _httpClient.Dispose();
    }

    private async Task AddTestUser()
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

        var response = await _httpClient.PostAsJsonAsync(Host + "/auth/create", args);
        response.EnsureSuccessStatusCode();
    }

    private async Task RemoveTestUser()
    {
        var email = TestEmail;
        var response = await _httpClient.PostAsJsonAsync(Host + "/auth/remove", email);
        response.EnsureSuccessStatusCode();
    }

    private async Task AddAuthHeader()
    {
        var token = await GetTokenForTestUser();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
    }

    public async Task AddAuthHeader(string email, string password)
    {
        var token = await GetTokenForUser(email, password);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
    }

    private async Task<string> GetTokenForTestUser()
    {
        return await GetTokenForUser(TestEmail, TestUserPassword);
    }

    private async Task<string> GetTokenForUser(string email, string password)
    {
        var form = new MultipartFormDataContent();
        form.Add(new StringContent(email), "email");
        form.Add(new StringContent(password), "password");

        var response = await _httpClient.PostAsync(Host + "/auth/login", form);
        var token = await response.Content.ReadAsStringAsync();

        return token;
    }
}