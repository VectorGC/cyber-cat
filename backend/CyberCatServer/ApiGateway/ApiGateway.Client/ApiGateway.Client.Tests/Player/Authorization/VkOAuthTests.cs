using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Player.Authorization
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
            var email = "test_vk_oauth@test.com";
            var userName = "TestVkOauth";
            var vkId = "123456789";

            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                var loginResult = await client.LoginPlayerWithVk(email, userName, vkId);
                Assert.IsTrue(loginResult.IsSuccess);
                Assert.AreEqual(email, loginResult.Value.User.Email);
                Assert.AreEqual(userName, loginResult.Value.User.FirstName);

                var doubleLoginResult = await client.LoginPlayerWithVk(email, userName, vkId);
                Assert.IsFalse(doubleLoginResult.IsSuccess);
                Assert.AreEqual("Сперва нужно выйти из текущей учетной записи", doubleLoginResult.Error);

                var logoutResult = await client.Player.Logout();
                Assert.IsTrue(logoutResult.IsSuccess);

                Assert.IsNull(client.Player);

                loginResult = await client.LoginPlayerWithVk(email, userName, vkId);
                await client.Player.Remove();

                Assert.IsNull(client.Player);
            }
        }
    }
}