using ApiGateway.Client.Internal;
using ApiGateway.Client.Internal.Serverless;

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
#if UNITY_WEBGL
            var webClient = new ApiGateway.Client.Internal.WebClientAdapters.UnityWebRequest.UnityWebClient();
#endif
#if WEB_CLIENT
            var webClient = new ApiGateway.Client.Internal.WebClientAdapters.WebClientAdapter.WebClientAdapter();
#endif
            return new AnonymousClientProxy(uri, webClient);
        }
    }
}