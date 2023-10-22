using System.Threading.Tasks;
using ApiGateway.Client.V2;
using ApiGateway.Client.V3;
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
            Assert.IsNotNull(user.Access<VK>());
            Assert.IsFalse(user.Access<VK>().IsSignedIn);
            await user.Access<VK>().SignIn(email, userName);
            Assert.IsTrue(user.Access<VK>().IsSignedIn);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            user.Access<VK>().SignOut();
            Assert.IsFalse(user.Access<VK>().IsSignedIn);
            Assert.AreEqual(string.Empty, user.Email);
            Assert.AreEqual(string.Empty, user.Name);

            user = client.User;
            Assert.IsNotNull(user.Access<VK>());
            Assert.IsFalse(user.Access<VK>().IsSignedIn);
            await user.Access<VK>().SignIn(email, userName);
            Assert.IsTrue(user.Access<VK>().IsSignedIn);
            Assert.IsNotNull(user.Access<VK>());
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            // Clean up.
            await user.Access<Dev>().RemoveUser(email);
            
            // Support web sockets.
            // Assert.IsFalse(user.Access<VK>().IsSignedIn);
        }
        
        [Test]
        public async Task SignUpViaOAuth_V3()
        {
            var email = "test_vk_oauth";
            var userName = "TestVkOauth";

            var client = new V2.ApiGateway.Client(_serverEnvironment);

            var user = client.UserV3;
            Assert.IsNotNull(user.Access<VK_V3>());
            Assert.IsFalse(user.Access<VK_V3>().IsSignedIn);
            await user.Access<VK_V3>().SignIn(email, userName);
            Assert.IsTrue(user.Access<VK_V3>().IsSignedIn);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            user.Access<VK_V3>().SignOut();
            Assert.IsFalse(user.Access<VK_V3>().IsSignedIn);
            Assert.AreEqual(string.Empty, user.Email);
            Assert.AreEqual(string.Empty, user.Name);

            user = client.UserV3;
            Assert.IsNotNull(user.Access<VK_V3>());
            Assert.IsFalse(user.Access<VK_V3>().IsSignedIn);
            await user.Access<VK_V3>().SignIn(email, userName);
            Assert.IsTrue(user.Access<VK_V3>().IsSignedIn);
            Assert.IsNotNull(user.Access<VK_V3>());
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(userName, user.Name);

            // Clean up.
            await user.Access<DevV3>().Users.RemoveByEmail(email);

            // Support web sockets.
            // Assert.IsFalse(user.Access<VK>().IsSignedIn);
        }
    }
}