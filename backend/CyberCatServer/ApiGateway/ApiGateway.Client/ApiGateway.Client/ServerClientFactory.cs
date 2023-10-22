using ApiGateway.Client.Internal;
using ApiGateway.Client.Internal.Serverless;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Internal.WebClientAdapters.WebClientAdapter;

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

            var uri = serverEnvironment.ToUri();
            var webClient = new WebClientAdapter();

            return new AnonymousClientProxy(uri, webClient);
        }
    }
}