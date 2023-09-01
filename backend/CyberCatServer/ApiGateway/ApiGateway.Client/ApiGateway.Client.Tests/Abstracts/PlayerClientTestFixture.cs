using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    public abstract class PlayerClientTestFixture : AuthorizedClientTestFixture
    {
        protected PlayerClientTestFixture(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        protected async Task<IPlayerClient> GetPlayerClient()
        {
            return await ServerClientFactory.CreatePlayer(ServerEnvironment, "test@test.com", "test_password");
        }

        [TearDown]
        public new async Task TearDown()
        {
            var client = await GetPlayerClient();
            await client.RemovePlayer();
        }
    }
}