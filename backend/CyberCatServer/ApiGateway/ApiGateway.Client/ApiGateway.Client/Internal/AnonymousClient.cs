using System.Threading.Tasks;

namespace ApiGateway.Client
{
    internal class AnonymousClient : IAnonymousClient
    {
        public IAuthorizationService Authorization => _client;

        private readonly Client _client;

        public AnonymousClient(Client client)
        {
            _client = client;
        }

        public async Task<string> GetStringTestAsync(string uri)
        {
            var client = WebClientFactory.Create();
            var response = await client.GetStringAsync(uri);

            return response;
        }
    }
}