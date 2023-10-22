using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Internal.WebClientAdapters.WebClientAdapter;
using Shared.Models;

namespace ApiGateway.Client.V2
{
    public class WebClientAccess : IAccess
    {
        public bool IsAvailable => _serverEnvironment != ServerEnvironment.Serverless;

        private readonly ServerEnvironment _serverEnvironment;
        private readonly IWebClientAdapter _webClient;
        private readonly Credentials _credentials;

        public WebClientAccess(ServerEnvironment serverEnvironment, Credentials credentials)
        {
            _credentials = credentials;
            _serverEnvironment = serverEnvironment;
            _webClient = new WebClientAdapter();

            _credentials.TokenChanged += OnTokenChanged;
        }

        public void Dispose()
        {
            _credentials.TokenChanged -= OnTokenChanged;
        }

        private void OnTokenChanged(string token)
        {
            const string authenticationScheme = "Bearer";

            if (string.IsNullOrEmpty(token))
            {
                _webClient.RemoveAuthorizationHeader();
                return;
            }

            _webClient.AddAuthorizationHeader(authenticationScheme, token);
        }

        public async Task<string> SignWithOAuth(string email, string userName)
        {
            var uri = _serverEnvironment.ToUri();
            var token = await _webClient.PostAsync(uri + "vk/signIn", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["name"] = userName
            });

            return token;
        }

        public async Task RemoveUser(string email)
        {
            var devKeyEncrypted = await Crypto.EncryptAsync("cyber-cat", "cyber");
            await _webClient.PostAsync(_serverEnvironment.ToUri() + "dev/users/remove", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["devKey"] = devKeyEncrypted
            });
        }
    }
}