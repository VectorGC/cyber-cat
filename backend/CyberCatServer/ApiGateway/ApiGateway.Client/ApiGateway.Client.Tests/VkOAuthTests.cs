using System.Threading.Tasks;
using ApiGateway.Client.V2;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class VkOAuthTests
    {
        private readonly ServerEnvironment _serverEnvironment;

        public VkOAuthTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        [Test]
        public async Task SignUpViaOAuth()
        {
            var email = "test_vk_oauth";
            var userName = "TestVkOauth";

            var client = new V2.ApiGateway.Client(_serverEnvironment);
            var user = client.User;

            Assert.IsFalse(user.Access<VK>().IsSignedIn);
            Assert.IsTrue(user.IsAnonymous);

            var success = await user.Access<VK>().SignIn(email, userName);

            Assert.IsTrue(success);
            Assert.IsTrue(user.Access<VK>().IsSignedIn);
            Assert.IsFalse(user.IsAnonymous);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            // Clean up.
            await user.Access<Dev>().RemoveUser(email);

            // TODO: Support web sockets.
            // Assert.IsFalse(user.Access<VK>().IsSignedIn);
            // Assert.IsTrue(user.IsAnonymous);
        }
    }
}