using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public class AnonymousClientTestFixture
    {
        private readonly ServerEnvironment _serverEnvironment;

        public AnonymousClientTestFixture(ServerEnvironment serverEnvironment)
        {
            _serverEnvironment = serverEnvironment;
        }

        public IAnonymous GetAnonymousClient()
        {
            return ServerClientFactory.CreateAnonymous(_serverEnvironment);
        }
    }
}