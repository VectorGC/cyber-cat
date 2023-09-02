using ApiGateway.Client.Internal.Anonymous;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client
{
    public static class ServerClientFactory
    {
        public static IAnonymous CreateAnonymous(ServerEnvironment serverEnvironment)
        {
            if (serverEnvironment == ServerEnvironment.Serverless)
            {
                return new AnonymousServerless();
            }

            var uri = ServerEnvironmentMap.Get(serverEnvironment);
            var webClient = WebClientFactory.Create();

            return new AnonymousClientProxy(uri, webClient);
        }
    }
}