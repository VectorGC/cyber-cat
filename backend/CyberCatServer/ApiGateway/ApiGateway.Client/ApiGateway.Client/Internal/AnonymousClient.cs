using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Clients;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.Internal
{
    internal class AnonymousClient : IAnonymousClient
    {
        private readonly IWebClient _webClient;
        private readonly Uri _uri;

        public AnonymousClient(Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
        }

        public async Task RegisterUser(string email, string password, string name)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password,
                ["name"] = name,
            };

            await _webClient.PostAsync(_uri + "auth/register", form);
        }

        public async Task<IAuthorizedClient> Authorize(string email, string password)
        {
            var token = await GetAuthenticationToken(email, password);
            return new AuthorizedClient(token, _uri, _webClient);
        }

        private async Task<string> GetAuthenticationToken(string email, string password)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password
            };

            var accessToken = await _webClient.PostAsync(_uri + "auth/login", form);
            return accessToken;
        }
    }
}