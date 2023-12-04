using System.Threading.Tasks;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class PlayerTests : ApiGatewayClientTestFixture
    {
        public PlayerTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task CreatePlayerAndRemovePlayer_WhenPassValidCredentials()
        {
            using (var client = await TestPlayerClientAsync())
            {
                var task = client.Player.Tasks["tutorial"];
                Assert.IsFalse(task.IsStarted);

                await client.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.IsStarted);
            }

            // Create player again.
            using (var anotherClient = await TestPlayerClientAsync())
            {
                // Progress has been lost.
                var task = anotherClient.Player.Tasks["tutorial"];
                Assert.IsFalse(task.IsStarted);

                await anotherClient.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.IsStarted);
            }
        }
    }
}