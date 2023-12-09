using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Player
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class PlayerTests
    {
        protected readonly string Email = "test@test.com";
        protected readonly string Password = "test_password";
        protected readonly string UserName = "Test_Name";

        private readonly ServerEnvironment _serverEnvironment;

        public PlayerTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        private async Task<TestPlayerClient> CreatePlayerClient()
        {
            var client = new ApiGatewayClient(_serverEnvironment);

            var result = await client.RegisterPlayer(Email, Password, UserName);
            if (!result.IsSuccess)
                throw new InvalidOperationException(result.Error);

            var loginResult = await client.LoginPlayer(Email, Password);
            if (!loginResult.IsSuccess)
                throw new InvalidOperationException(loginResult.Error);

            return new TestPlayerClient(client);
        }

        [Test]
        public async Task CreatePlayerAndRemovePlayer_WhenPassValidCredentials()
        {
            using (var client = await CreatePlayerClient())
            {
                var task = client.Client.Player.Tasks["tutorial"];
                Assert.IsFalse(task.IsStarted);

                await client.Client.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.IsStarted);
            }

            // Create player again.
            using (var anotherClient = await CreatePlayerClient())
            {
                // Progress has been lost.
                var task = anotherClient.Client.Player.Tasks["tutorial"];
                Assert.IsFalse(task.IsStarted);

                await anotherClient.Client.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.IsStarted);
            }
        }
    }
}