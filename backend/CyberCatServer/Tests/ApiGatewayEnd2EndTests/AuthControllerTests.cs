using System.Net.Http.Json;
using ApiGatewayEnd2EndTests.Extensions;
using Shared.Dto;

namespace ApiGatewayEnd2EndTests;

public class AuthControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
        _client = new HttpClient();
    }

    [Test]
    public async Task Login_WhenPassValidCredentials()
    {
        var token = await AuthHttpClientExtensions.GetToken(_client, "karo@test.ru", "12qw!@QW");

        Assert.IsNotEmpty(token);
    }

    [Test]
    public async Task AuthorizePlayer_WhenPassValidCredentials()
    {
        await _client.AddJwtAuthorizationHeaderAsync("karo@test.ru", "12qw!@QW");

        var response = await _client.GetAsync("http://localhost:5000/auth/authorize_player");
        var responseDto = await response.Content.ReadFromJsonAsync<AuthorizePlayerResponse>();

        Assert.AreEqual("lll", responseDto.Name);
    }
}