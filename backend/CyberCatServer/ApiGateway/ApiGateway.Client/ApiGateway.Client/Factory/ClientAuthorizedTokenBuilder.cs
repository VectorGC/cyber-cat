using ApiGateway.Client.Internal;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.Factory
{
    public readonly struct ClientAuthorizedTokenBuilder
    {
        private readonly string _token;

        public ClientAuthorizedTokenBuilder(string token)
        {
            _token = token;
        }

        public IAuthorizedClient Create(ServerEnvironment serverEnvironment)
        {
            if (serverEnvironment == ServerEnvironment.Serverless)
            {
                return new ServerlessClient();
            }

            var uri = ServerEnvironmentMap.Get(serverEnvironment);
            var webClient = WebClientFactory.Create();

            return new AuthorizedClient(uri, _token, webClient);
        }
    }
}