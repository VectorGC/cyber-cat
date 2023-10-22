using System.Threading.Tasks;
using ApiGateway.Client.V2;
using ApiGateway.Client.V2.Access;
using ApiGateway.Client.V2.Access.Dev;
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
            Assert.IsNotNull(user.Access<Vk>());
            Assert.IsFalse(user.Access<Vk>().IsSignedIn);
            await user.Access<Vk>().SignIn(email, userName);
            Assert.IsTrue(user.Access<Vk>().IsSignedIn);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            user.Access<Vk>().SignOut();
            Assert.IsFalse(user.Access<Vk>().IsSignedIn);
            Assert.AreEqual(string.Empty, user.Email);
            Assert.AreEqual(string.Empty, user.Name);

            user = client.User;
            Assert.IsNotNull(user.Access<Vk>());
            Assert.IsFalse(user.Access<Vk>().IsSignedIn);
            await user.Access<Vk>().SignIn(email, userName);
            Assert.IsTrue(user.Access<Vk>().IsSignedIn);
            Assert.IsNotNull(user.Access<Vk>());
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            // Clean up.
            await user.Access<Dev>().Users.RemoveByEmail(email);

            // Support web sockets.
            // Assert.IsFalse(user.Access<VK>().IsSignedIn);
        }
    }
}