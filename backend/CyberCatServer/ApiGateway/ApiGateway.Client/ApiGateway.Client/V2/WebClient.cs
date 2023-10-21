using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;

namespace ApiGateway.Client.V2
{
    public class WebClient : IAccess
    {
        public bool IsAvailable => _serverEnvironment != ServerEnvironment.Serverless;

        private readonly ServerEnvironment _serverEnvironment;
        private readonly IWebClient _webClient;
        private readonly Credentials _credentials;

        public WebClient(ServerEnvironment serverEnvironment, Credentials credentials)
        {
            _credentials = credentials;
            _serverEnvironment = serverEnvironment;
            _webClient = WebClientFactory.Create();

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
            var uri = _serverEnvironment.GetUri();
            var token = await _webClient.PostAsync(uri + "OAuth/signIn", new Dictionary<string, string>()
            {
                ["email"] = email,
                ["name"] = userName
            });

            return token;
        }

        public async Task RemoveUser(string email)
        {
            await _webClient.PostAsync(_serverEnvironment.GetUri() + "auth/dev/remove", new Dictionary<string, string>()
            {
                ["userEmail"] = email,
                ["key"] = "cyber"
            });
        }
    }
}