using ApiGateway.Client.Internal;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.Factory
{
    public static class ServerClientFactory
    {
        public static ClientAuthorizedTokenBuilder UseToken(string token) => new ClientAuthorizedTokenBuilder(token);
        public static ClientAuthorizedCredentialsBuilder UseCredentials(string email, string password) => new ClientAuthorizedCredentialsBuilder(email, password);
        public static ClientAuthorizedCredentialsBuilder UseUniversalCredentials() => new ClientAuthorizedCredentialsBuilder("cat", "cat");

        public static IAnonymousClient Create(ServerEnvironment serverEnvironment)
        {
            if (serverEnvironment == ServerEnvironment.Serverless)
            {
                return new ServerlessClient();
            }

            var uri = ServerEnvironmentMap.Get(serverEnvironment);
            var webClient = WebClientFactory.Create();

            return new AnonymousClient(uri, webClient);
        }
    }
}