using System.Net;
using System.Threading.Tasks;
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
        public async Task CreatePlayerAndRemovePlayer_WhenPassValidCredentials()
        {
            var authorized = await GetAuthorizedClient();

            var ex = Assert.ThrowsAsync<WebException>(async () => await authorized.AuthorizePlayer());
            var response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            await authorized.RegisterPlayer();

            // Player is already registered.
            ex = Assert.ThrowsAsync<WebException>(async () => await authorized.RegisterPlayer());
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);

            var client = await authorized.AuthorizePlayer();

            Assert.IsNotNull(client);

            await client.RemovePlayer();

            ex = Assert.ThrowsAsync<WebException>(async () => await authorized.AuthorizePlayer());
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            ex = Assert.ThrowsAsync<WebException>(async () => await client.PlayerService.VerifySolution("tutorial", "Hello"));
            response = (HttpWebResponse) ex.Response;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
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