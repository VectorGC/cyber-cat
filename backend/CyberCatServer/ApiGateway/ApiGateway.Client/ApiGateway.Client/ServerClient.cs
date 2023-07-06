using ApiGateway.Client.Internal;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client
{
    public static class ServerClient
    {
        public static IAnonymousClient Create(string uri)
        {
            var restClient = WebClientFactory.Create();
            var client = new Internal.Client(uri, restClient);

            return new AnonymousClient(client);
        }

        public static IAuthorizedClient Create(string uri, string token)
        {
            var restClient = WebClientFactory.Create();
            var client = new Internal.Client(uri, restClient);

            return new AuthorizedClient(client, token);
        }

        public static IAnonymousClient CreateAnonymousServerless()
        {
            return new ServerlessClient();
        }

        public static IAuthorizedClient CreateAuthorizedServerless()
        {
            return new ServerlessClient();
        }
    }
}