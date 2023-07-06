using ApiGateway.Client.Services.Authorization;

namespace ApiGateway.Client.Internal
{
    internal class AnonymousClient : IAnonymousClient
    {
        public IAuthorizationService Authorization => _client;

        private readonly Client _client;

        public AnonymousClient(Client client)
        {
            _client = client;
        }
    }
}