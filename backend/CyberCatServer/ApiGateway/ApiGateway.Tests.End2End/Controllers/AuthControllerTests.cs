using ApiGateway.Tests.End2End.Extensions;

namespace ApiGateway.Tests.End2End.Controllers;

[TestFixture]
public class AuthControllerTests : E2ETests
{
    [Test]
    public async Task Login_WhenPassValidCredentials()
    {
        var token = await Client.GetTokenForTestUser();

        Assert.IsNotEmpty(token);
    }

    [Test]
    public async Task AuthorizePlayer_WhenPassValidCredentials()
    {
        var name = await Client.GetStringAsync("/auth/authorize_player");

        Assert.AreEqual(TestUserHttpClient.TestUserName, name);
    }

    [Test]
    public async Task ExistsDefaultUser_WhenPassValidCredentials()
    {
        await Client.AddAuthHeader("cyber@cat", "Cyber_Cat123@");
        var name = await Client.GetStringAsync("/auth/authorize_player");

        Assert.AreEqual("CyberCat", name);
    }
}