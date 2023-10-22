using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using Shared.Models.Models.AuthorizationTokens;

namespace ApiGateway.Client.V2.Access
{
    public class Vk : IAccess
    {
        public bool IsAvailable => true;
        public bool IsSignedIn => _credentials.AuthorizationTokenHolder.Token is VkToken;

        private readonly Credentials _credentials;
        private readonly WebClient _webClient;

        internal Vk(Credentials credentials, WebClient webClient)
        {
            _credentials = credentials;
            _webClient = webClient;
        }

        public async Task SignIn(string email, string userName)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var token = await _webClient.PostAsync<VkToken>("vk/signin", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["name"] = userName
            });

            _credentials.AuthorizationTokenHolder.Token = token;
            _credentials.Email = email;
            _credentials.Name = userName;
        }

        public void SignOut()
        {
            _credentials.AuthorizationTokenHolder.Token = null;
            _credentials.Email = string.Empty;
            _credentials.Name = string.Empty;
        }
    }
}