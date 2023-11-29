using System.Threading.Tasks;
using ApiGateway.Client.V3.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.V2
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class AuthenticateTestsV2
    {
        private readonly ServerEnvironment _serverEnvironment;
        private const string Email = "test@test.com";
        private const string Password = "test_password";
        private const string UserName = "Test_User";
        private const string WrongPassword = "wrong_test_password";

        public AuthenticateTestsV2(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        [Test]
        public async Task RegistrationPlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                var registerResult = await client.PlayerService.RegisterPlayer(Email, Password, UserName);
                Assert.IsTrue(registerResult.IsSuccess);

                var doubleRegisterResult = await client.PlayerService.RegisterPlayer(Email, Password, UserName);
                Assert.IsFalse(doubleRegisterResult.IsSuccess);
                Assert.AreEqual("User is already registered", doubleRegisterResult.Error);

                var removeResult = client.PlayerService.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }

        [Test]
        public async Task LoginPlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                Assert.IsNull(client.Player);

                var loginWithoutRegisterResult = await client.PlayerService.LoginPlayer(Email, Password);
                Assert.IsFalse(loginWithoutRegisterResult.IsSuccess);
                Assert.AreEqual($"Not found user '{UserName}'", loginWithoutRegisterResult.Error);

                await client.PlayerService.RegisterPlayer(Email, Password, UserName);

                var wrongLoginAfterRegisterResult = await client.PlayerService.LoginPlayer(Email, WrongPassword);
                Assert.IsFalse(wrongLoginAfterRegisterResult.IsSuccess);
                Assert.AreEqual("Wrong password", wrongLoginAfterRegisterResult.Error);

                var loginResult = await client.PlayerService.LoginPlayer(Email, Password);
                Assert.IsTrue(loginResult.IsSuccess);
                Assert.IsNotNull(client.Player);
                Assert.AreEqual(Email, client.Player.User.Email);
                Assert.AreEqual(UserName, client.Player.User.Name);

                var removeResult = client.PlayerService.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }

        [Test]
        public async Task LogoutPlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                await client.PlayerService.RegisterPlayer(Email, Password, UserName);

                var logoutWithoutLoginResult = client.PlayerService.Logout();
                Assert.IsFalse(logoutWithoutLoginResult.IsSuccess);
                Assert.AreEqual("First, you need to log in to the account", logoutWithoutLoginResult.Error);

                var logoutResult = client.PlayerService.Logout();
                Assert.IsTrue(logoutResult.IsSuccess);

                var removeResult = client.PlayerService.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }

        [Test]
        public async Task RemovePlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                var removeWithoutRegister = client.PlayerService.Remove();
                Assert.IsFalse(removeWithoutRegister.IsSuccess);
                Assert.AreEqual($"First, you need to log in to the account", removeWithoutRegister.Error);

                await client.PlayerService.RegisterPlayer(Email, Password, UserName);
                await client.PlayerService.LoginPlayer(Email, Password);

                var removeResult = client.PlayerService.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }
    }
}