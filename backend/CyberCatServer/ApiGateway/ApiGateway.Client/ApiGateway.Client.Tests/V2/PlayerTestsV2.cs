using System.Threading.Tasks;
using ApiGateway.Client.Tests.V2.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.V2
{
    public class PlayerTestsV2 : ApiGatewayClientTestFixture
    {
        public PlayerTestsV2(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task CreatePlayerAndRemovePlayer_WhenPassValidCredentials()
        {
            using (var client = await GetTestPlayerClient())
            {
                var task = client.Player.Tasks["tutorial"];
                Assert.IsFalse(task.StatusType.IsStarted);

                await client.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.StatusType.IsStarted);
            }

            // Create player again.
            using (var anotherClient = await GetTestPlayerClient())
            {
                // Progress has been lost.
                var task = anotherClient.Player.Tasks["tutorial"];
                Assert.IsFalse(task.StatusType.IsStarted);

                await anotherClient.Player.Tasks[task.Id].SubmitSolution("Hello");
                Assert.IsTrue(task.StatusType.IsStarted);
            }
        }
    }
}