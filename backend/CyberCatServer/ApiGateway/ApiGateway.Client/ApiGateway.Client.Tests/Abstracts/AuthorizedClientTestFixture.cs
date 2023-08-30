using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    public abstract class AuthorizedClientTestFixture : AnonymousClientTestFixture
    {
        protected AuthorizedClientTestFixture(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        private async Task<IAuthorizedClient> GetAuthorizedClient()
        {
            return await ServerClientFactory.CreateAuthorized(ServerEnvironment);
        }

        [TearDown]
        public async Task TearDown()
        {
            var client = await GetAuthorizedClient();
            await client.RemoveUser();
        }
    }
}