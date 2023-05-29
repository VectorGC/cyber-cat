using ApiGateway.Tests.End2End.Extensions;

namespace ApiGateway.Tests.End2End;

[TestFixture]
public class AuthControllerTests : E2ETests
{
    [Test]
    public async Task Login_WhenPassValidCredentials()
    {
        var client = new HttpClient();
        var token = await TestUserHttpClient.GetTokenForTestUser(client);

        Assert.IsNotEmpty(token);
    }

    [Test]
    public async Task AuthorizePlayer_WhenPassValidCredentials()
    {
        var name = await Client.GetStringAsync("http://localhost:5000/auth/authorize_player");

        Assert.AreEqual(TestUserHttpClient.TestUserName, name);
    }
}