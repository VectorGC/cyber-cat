using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;

namespace ApiGateway.Client.Internal
{
    internal class AnonymousClient : IAnonymousClient, IAuthorizationService
    {
        public IAuthorizationService Authorization => this;

        private readonly IWebClient _webClient;
        private readonly Uri _uri;

        public AnonymousClient(Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
        }

        async Task<string> IAuthorizationService.GetAuthenticationToken(string email, string password)
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