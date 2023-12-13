using System.Threading.Tasks;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Player
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class PlayerTests
    {
        private readonly ServerEnvironment _serverEnvironment;

        public PlayerTests(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        [Test]
        public async Task CreatePlayerAndRemovePlayer_WhenPassValidCredentials()
        {
            using (var client = await TestPlayerClient.Create(_serverEnvironment))
            {
                var task = client.Client.Player.Tasks["tutorial"];
                Assert.IsFalse(task.IsStarted);

                await client.Client.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.IsStarted);
            }

            // Create player again.
            using (var anotherClient = await TestPlayerClient.Create(_serverEnvironment))
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