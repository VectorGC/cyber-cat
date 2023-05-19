using ApiGatewayEnd2EndTests.Extensions;

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

        var name = await _client.GetStringAsync("http://localhost:5000/auth/authorize_player");

        Assert.AreEqual("lll", name);
    }
}