using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Tests.Abstracts;
using ApiGateway.Client.Tests.Extensions;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class PlayerTests : UserClientTestFixture
    {
        public PlayerTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task CreatePlayerAndRemovePlayer_WhenPassValidCredentials()
        {
            var user = await GetUserClient();

            var player = await user.SignInAsPlayer();
            Assert.IsNotNull(player);

            var task = player.Tasks["tutorial"];
            Assert.IsAssignableFrom<NotStarted>(await task.GetStatus());

            await task.VerifySolution("Hello");
            Assert.IsAssignableFrom<HaveSolution>(await task.GetStatus());

            await player.Remove();

            await AssertAsync.ThrowsWebException(async () => await task.VerifySolution("Hello"), HttpStatusCode.NotFound);

            // Create player again.
            player = await user.SignInAsPlayer();
            Assert.IsNotNull(player);

            // Progress has been lost.
            Assert.IsAssignableFrom<NotStarted>(await task.GetStatus());

            await player.Remove();

            await AssertAsync.ThrowsWebException(async () => await player.Remove(), HttpStatusCode.NotFound);
        }

        /*
        [Test]
        public async Task CreatePlayerAndOperateWithBtc()
        {
            var client = await GetClient();

            var player = await client.PlayerService.AuthorizePlayer();

            var player = await client.PlayerService.GetPlayer();
            Assert.IsNotNull(player);

            await client.PlayerService.AddBitcoins(1000);
            player = await client.PlayerService.GetPlayer();
            Assert.IsNotNull(player);
            Assert.That(player.BitcoinsAmount, Is.EqualTo(1000));

            await client.PlayerService.RemoveBitcoins(200);
            player = await client.PlayerService.GetPlayer();
            Assert.IsNotNull(player);
            Assert.That(player.BitcoinsAmount, Is.EqualTo(800));

            await client.PlayerService.RemovePlayer();
        }
        */
    }
}