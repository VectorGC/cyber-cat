using System.Threading.Tasks;
using ApiGateway.Client.Models;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    public abstract class PlayerClientTestFixture : UserClientTestFixture
    {
        protected PlayerClientTestFixture(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        protected async Task<IPlayer> GetPlayerClient()
        {
            var client = await GetUserClient();
            return await client.SignInAsPlayer();
        }

        protected override async Task OnTearDown()
        {
            var client = await GetPlayerClient();
            await client.Remove();

            await base.OnTearDown();
        }
    }
}