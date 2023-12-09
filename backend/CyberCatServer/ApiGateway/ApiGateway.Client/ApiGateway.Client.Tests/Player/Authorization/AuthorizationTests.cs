using System.Threading.Tasks;
using ApiGateway.Client.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class AuthorizationTests
    {
        private readonly ServerEnvironment _serverEnvironment;
        private const string Email = "test@test.com";
        private const string Password = "test_password";
        private const string UserName = "Дмитрий";
        private const string WrongPassword = "wrong_test_password";

        public AuthorizationTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        [Test]
        public async Task RegistrationPlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                var registerResult = await client.RegisterPlayer(Email, Password, UserName);
                Assert.IsNull(registerResult.Error);

                var doubleRegisterResult = await client.RegisterPlayer(Email, Password, UserName);
                Assert.IsFalse(doubleRegisterResult.IsSuccess);
                Assert.AreEqual($"Email '{Email}' уже зарегистрирован", doubleRegisterResult.Error);

                var loginResult = await client.LoginPlayer(Email, Password);
                Assert.IsTrue(loginResult.IsSuccess);
                Assert.IsNotNull(client.Player);
                Assert.AreEqual(Email, client.Player.User.Email);
                Assert.AreEqual(UserName, client.Player.User.FirstName);

                var removeResult = client.Player.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }

        [Test]
        public async Task LoginPlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                Assert.IsNull(client.Player);

                var loginWithoutRegisterResult = await client.LoginPlayer(Email, Password);
                Assert.IsFalse(loginWithoutRegisterResult.IsSuccess);
                Assert.AreEqual("Пользователь не найден", loginWithoutRegisterResult.Error);

                await client.RegisterPlayer(Email, Password, UserName);

                var wrongLoginAfterRegisterResult = await client.LoginPlayer(Email, WrongPassword);
                Assert.IsFalse(wrongLoginAfterRegisterResult.IsSuccess);
                Assert.AreEqual("Неверный пароль", wrongLoginAfterRegisterResult.Error);

                var loginResult = await client.LoginPlayer(Email, Password);
                Assert.IsTrue(loginResult.IsSuccess);
                Assert.IsNotNull(client.Player);
                Assert.AreEqual(Email, client.Player.User.Email);
                Assert.AreEqual(UserName, client.Player.User.FirstName);

                var doubleLoginResult = await client.LoginPlayer(Email, Password);
                Assert.IsFalse(doubleLoginResult.IsSuccess);
                Assert.AreEqual("Сперва нужно выйти из текущей учетной записи", doubleLoginResult.Error);

                var removeResult = client.Player.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }

        [Test]
        public async Task LogoutPlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                await client.RegisterPlayer(Email, Password, UserName);
                await client.LoginPlayer(Email, Password);

                var logoutResult = client.Player.Logout();
                Assert.IsTrue(logoutResult.IsSuccess);

                Assert.IsNull(client.Player);

                await client.LoginPlayer(Email, Password);

                var removeResult = client.Player.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }

        [Test]
        public async Task RemovePlayer()
        {
            using (var client = new ApiGatewayClient(_serverEnvironment))
            {
                Assert.IsNull(client.Player);

                await client.RegisterPlayer(Email, Password, UserName);
                await client.LoginPlayer(Email, Password);

                var removeResult = client.Player.Remove();
                Assert.IsTrue(removeResult.IsSuccess);
            }
        }
    }
}