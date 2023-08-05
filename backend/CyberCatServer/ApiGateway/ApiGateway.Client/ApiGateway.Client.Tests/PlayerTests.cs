using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public async Task CreatePlayerAndOperateWithBtc()
        {
            var client = await TestClient.TestClient.Authorized();
            
            await client.PlayerService.AddNewPlayer();

            var player = await client.PlayerService.GetPlayerById();
            Assert.IsNotNull(player);

            await client.PlayerService.AddBitcoinsToPlayer(1000);
            player = await client.PlayerService.GetPlayerById();
            Assert.IsNotNull(player);
            Assert.That(player.BitcoinCount, Is.EqualTo(1000));

            await client.PlayerService.TakeBitcoinsFromPlayer(200);
            player = await client.PlayerService.GetPlayerById();
            Assert.IsNotNull(player);
            Assert.That(player.BitcoinCount, Is.EqualTo(800));

            await client.PlayerService.DeletePlayer();
        }
        
    }
}