using ApiGateway.Client.Services;

namespace ApiGateway.Client.Internal
{
    internal class AuthorizedClient : IAuthorizedClient
    {
        private readonly Client _client;

        public ITaskRepository Tasks => _client;

        public ISolutionService SolutionService => _client;

        public IJudgeService JudgeService => _client;
        public IPlayerService PlayerService => _client;

        public AuthorizedClient(Client client, string token)
        {
            _client = client;
            client.AddAuthorizationToken(token);
        }
    }
}