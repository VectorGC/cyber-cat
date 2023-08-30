using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using ApiGateway.Client.Internal;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client
{
    public static class ServerClientFactory
    {
        public static IAnonymousClient CreateAnonymous(ServerEnvironment serverEnvironment)
        {
            if (serverEnvironment == ServerEnvironment.Serverless)
            {
                return new ServerlessClient();
            }

            var uri = ServerEnvironmentMap.Get(serverEnvironment);
            var webClient = WebClientFactory.Create();

            return new AnonymousClient(uri, webClient);
        }

        public static async Task<IAuthorizedClient> CreateAuthorized(ServerEnvironment serverEnvironment, string email = "cat", string password = "cat")
        {
            var anonymous = CreateAnonymous(serverEnvironment);
            return await anonymous.Authorize(email, password);
        }

        public static async Task<IPlayerClient> CreatePlayer(ServerEnvironment serverEnvironment, string email = "cat", string password = "cat")
        {
            var authorized = await CreateAuthorized(serverEnvironment, email, password);
            return await authorized.AuthorizePlayer();
        }
    }
}