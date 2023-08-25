using System.Threading.Tasks;
using ApiGateway.Client.Factory;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    public class PlayerTests : AuthorizedClientTestFixture
    {
        public PlayerTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task CreatePlayerAndOperateWithBtc()
        {
            var client = await GetClient();

            await client.PlayerService.CreatePlayer();

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
    }
}