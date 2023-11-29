using System.Net;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Tests.Abstracts;
using ApiGateway.Client.Tests.Extensions;
using ApiGateway.Client.Tests.V2.Extensions;
using ApiGateway.Client.V3.Application;
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
                Assert.IsFalse(task.Status.IsStarted);

                await client.Client.TaskService.SubmitSolution(task.Id, "Hello");
                Assert.IsTrue(task.Status.IsStarted);

                client.Client.PlayerService.Remove();
                var result = await client.Client.TaskService.SubmitSolution(task.Id, "Hello");
                Assert.AreEqual("Player not found", result.Error);

                // Create player again.
                using (var anotherClient = await GetTestPlayerClient())
                {
                    // Progress has been lost.
                    task = anotherClient.Player.Tasks["tutorial"];
                    Assert.IsFalse(task.Status.IsStarted);
                }
            }
        }
    }
}