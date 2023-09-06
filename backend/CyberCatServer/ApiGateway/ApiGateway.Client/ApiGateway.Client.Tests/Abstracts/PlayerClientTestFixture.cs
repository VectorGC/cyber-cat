using System.Threading.Tasks;
using ApiGateway.Client.Models;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class PlayerClientTestFixture
    {
        private readonly UserClientTestFixture _userClientTestFixture;

        protected PlayerClientTestFixture(ServerEnvironment serverEnvironment)
        {
            _userClientTestFixture = new UserClientTestFixture(serverEnvironment);
        }

        protected async Task<IPlayer> GetPlayerClient()
        {
            var client = await _userClientTestFixture.GetUserClient();
            return await client.SignInAsPlayer();
        }

        [TearDown]
        public async Task TearDown()
        {
            var client = await GetPlayerClient();
            await client.Remove();

            await _userClientTestFixture.TearDown();
        }
    }
}