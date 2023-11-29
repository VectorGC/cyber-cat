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
        private readonly WebClientV1 _webClientV1;

        internal Vk(Credentials credentials, WebClientV1 webClientV1)
        {
            _credentials = credentials;
            _webClientV1 = webClientV1;
        }

        public async Task SignIn(string email, string userName)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var token = await _webClientV1.PostAsync<VkToken>("vk/signin", new Dictionary<string, string>()
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