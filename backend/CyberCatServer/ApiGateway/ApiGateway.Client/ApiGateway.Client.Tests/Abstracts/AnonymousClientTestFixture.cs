using ApiGateway.Client.Factory;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public abstract class AnonymousClientTestFixture
    {
        protected readonly IAnonymousClient Client;

        protected AnonymousClientTestFixture(ServerEnvironment serverEnvironment)
        {
            Client = ServerClientFactory.Create(serverEnvironment);
        }
    }
}