using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using Shared.Models.Models.AuthorizationTokens;

namespace ApiGateway.Client.V3
{
    public class VK_V3 : IAccessV3
    {
        public bool IsAvailable => true;
        public bool IsSignedIn => _credentials.AuthorizationTokenHolder.Token is VkToken;

        private readonly CredentialsV3 _credentials;
        private readonly WebClient _webClient;

        internal VK_V3(CredentialsV3 credentials, WebClient webClient)
        {
            _credentials = credentials;
            _webClient = webClient;
        }

        public async Task SignIn(string email, string userName)
        {
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