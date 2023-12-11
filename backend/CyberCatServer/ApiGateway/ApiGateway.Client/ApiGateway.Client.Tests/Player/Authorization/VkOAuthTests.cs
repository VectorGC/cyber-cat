using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Player.Authorization
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class VkOAuthTests : PlayerClientTestFixture
    {
        public VkOAuthTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task SignUpViaOAuth()
        {
            var email = "test_vk_oauth@test.com";
            var userName = "TestVkOauth";
            var vkId = "123456789";

            using (var client = new ApiGatewayClient(ServerEnvironment))
            {
                Assert.IsNull(client.Player);

                var loginResult = await client.LoginPlayerWithVk(email, userName, vkId);
                Assert.IsTrue(loginResult.IsSuccess);
                Assert.AreEqual(email,  loginResult.Value.User.Email);
                Assert.AreEqual(userName, loginResult.Value.User.FirstName);

                var doubleLoginResult = await client.LoginPlayerWithVk(email, userName, vkId);
                Assert.IsFalse(doubleLoginResult.IsSuccess);
                Assert.AreEqual("Сперва нужно выйти из текущей учетной записи", doubleLoginResult.Error);

                var logoutResult = client.Player.Logout();
                Assert.IsTrue(logoutResult.IsSuccess);

                Assert.IsNull(client.Player);

                loginResult = await client.LoginPlayerWithVk(email, userName, vkId);
                client.Player.Remove();

                Assert.IsNull(client.Player);
            }
        }
    }
}