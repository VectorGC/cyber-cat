using System;
using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using ApiGateway.Client.Internal.Authorization;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.Internal
{
    internal class AuthorizedClient : IAuthorizedClient
    {
        private readonly IWebClient _webClient;
        private readonly Uri _uri;

        public AuthorizedClient(string token, Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
            _webClient.AddAuthorizationHeader(JwtBearerDefaults.AuthenticationScheme, token);
        }

        public async Task RegisterPlayer()
        {
            await _webClient.PostAsync(_uri + "player/register");
        }

        public async Task<IPlayerClient> AuthorizePlayer()
        {
            await _webClient.PostAsync(_uri + "player/authorize");
            return new PlayerClient(_uri, _webClient);
        }

        public async Task RemoveUser()
        {
            await _webClient.DeleteAsync(_uri + "auth");
        }
    }
}