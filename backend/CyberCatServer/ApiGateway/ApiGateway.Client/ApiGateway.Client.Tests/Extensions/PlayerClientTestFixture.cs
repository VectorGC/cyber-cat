using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Extensions
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public abstract class PlayerClientTestFixture
    {
        protected readonly ServerEnvironment ServerEnvironment;

        protected readonly string Email = "test@test.com";
        protected readonly string Password = "test_password";
        protected readonly string UserName = "Test_Name";

        protected ApiGatewayClient Client => _testPlayerClient.Client;
        private TestPlayerClient _testPlayerClient;

        public PlayerClientTestFixture(ServerEnvironment serverEnvironment)
        {
            ServerEnvironment = serverEnvironment;
        }

        public PlayerClientTestFixture(ServerEnvironment serverEnvironment, string email = "test@test.com", string password = "test_password", string userName = "Test_Name")
        {
            UserName = userName;
            Password = password;
            Email = email;
            ServerEnvironment = serverEnvironment;
        }

        [SetUp]
        public void SetUp()
        {
            _testPlayerClient = CreatePlayerClient().Result;
        }

        [TearDown]
        public void TearDown()
        {
            _testPlayerClient?.Dispose();
        }

        protected async Task<TestPlayerClient> CreatePlayerClient()
        {
            var client = new ApiGatewayClient(ServerEnvironment);

            var result = await client.RegisterPlayer(Email, Password, UserName);
            if (!result.IsSuccess)
                throw new InvalidOperationException(result.Error);

            var loginResult = await client.LoginPlayer(Email, Password);
            if (!loginResult.IsSuccess)
                throw new InvalidOperationException(loginResult.Error);

            return new TestPlayerClient(client);
            ;
        }
    }
}