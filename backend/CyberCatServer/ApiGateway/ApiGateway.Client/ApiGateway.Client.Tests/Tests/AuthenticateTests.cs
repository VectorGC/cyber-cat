using System.Threading.Tasks;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Tests
{
    [TestFixture]
    public class AuthenticateTests
    {
        [Test]
        public async Task AuthenticateDefaultUser_WhenPassValidCredentials()
        {
            var client = await PlayerClient.Create();
            var token = await client.Authenticate(PlayerClient.TestEmail, PlayerClient.TestUserPassword);

            Assert.IsNotEmpty(token);

            client.AddAuthorizationToken(token);
            var name = await client.AuthorizePlayer(token);

            Assert.AreEqual("Cat", name);
        }

        [Test]
        public async Task Authenticate_WhenUseTestClient()
        {
            var client = await PlayerClient.Create();
            var token = await client.Authenticate(PlayerClient.TestEmail, PlayerClient.TestUserPassword);
            var name = await client.AuthorizePlayer(token);

            Assert.AreEqual("Cat", name);
        }
    }
}