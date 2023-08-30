using ApiGateway.Client.Clients;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Abstracts
{
    [TestFixture(ServerEnvironment.Localhost, Category = "Localhost")]
    [TestFixture(ServerEnvironment.Production, Explicit = true, Category = "Production")]
    public abstract class AnonymousClientTestFixture
    {
        protected readonly ServerEnvironment ServerEnvironment;

        protected AnonymousClientTestFixture(ServerEnvironment serverEnvironment)
        {
            ServerEnvironment = serverEnvironment;
        }

        protected IAnonymousClient GetAnonymousClient()
        {
            return ServerClientFactory.CreateAnonymous(ServerEnvironment);
        }
    }
}