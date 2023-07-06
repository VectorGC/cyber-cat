namespace ApiGateway.Client
{
    internal class AuthorizedClient : IAuthorizedClient
    {
        private readonly Client _client;

        public ITaskRepository Tasks => _client;

        public ISolutionService SolutionService => _client;

        public IJudgeService JudgeService => _client;

        public AuthorizedClient(Client client, string token)
        {
            _client = client;
            client.AddAuthorizationToken(token);
        }
    }
}