using System.Threading.Tasks;
using ApiGateway.Client.Factory;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public abstract class AuthorizedClientTestFixture
    {
        private readonly ServerEnvironment _serverEnvironment;

        protected AuthorizedClientTestFixture(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        public async Task<IAuthorizedClient> GetClient()
        {
            return await ServerClientFactory.UseUniversalCredentials().Create(_serverEnvironment);
        }
    }
}