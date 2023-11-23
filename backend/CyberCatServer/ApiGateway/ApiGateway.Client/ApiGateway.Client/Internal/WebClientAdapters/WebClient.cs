using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;
using Shared.Models.Models.AuthorizationTokens;

namespace ApiGateway.Client.Internal.WebClientAdapters
{
    internal class WebClient : IDisposable
    {
        private readonly IWebClientAdapter _webClient =
#if UNITY_WEBGL
            new UnityWebRequest.UnityWebClient();
#endif
#if WEB_CLIENT
            new WebClientAdapter.WebClientAdapter();
#endif

        private readonly AuthorizationTokenHolder _tokenHolder;
        private readonly ServerEnvironment _serverEnvironment;

        public WebClient(ServerEnvironment serverEnvironment, AuthorizationTokenHolder tokenHolder)
        {
            _serverEnvironment = serverEnvironment;
            _tokenHolder = tokenHolder;
            _tokenHolder.TokenChanged += OnTokenChanged;
        }

        public void Dispose()
        {
            _tokenHolder.TokenChanged -= OnTokenChanged;
            _webClient.Dispose();
        }

        private void OnTokenChanged(AuthorizationToken token)
        {
            _webClient.RemoveAuthorizationHeader();
            if (token != null)
            {
                _webClient.AddAuthorizationHeader(token.Type, token.Value);
            }
        }

        public Task<string> GetAsync(string path)
        {
            return _webClient.GetStringAsync(_serverEnvironment.ToUri(path));
        }

        public Task<TResponse> GetAsync<TResponse>(string path)
        {
            return _webClient.GetFromFastJsonPolymorphicAsync<TResponse>(_serverEnvironment.ToUri(path));
        }

        public Task<string> PostAsync(string path, Dictionary<string, string> form)
        {
            return _webClient.PostAsync(_serverEnvironment.ToUri(path), form);
        }

        public Task<string> PostAsync(string path)
        {
            return _webClient.PostAsync(_serverEnvironment.ToUri(path));
        }

        public Task<TResponse> PostAsync<TResponse>(string path, Dictionary<string, string> form)
        {
            return _webClient.PostAsFastJsonPolymorphicAsync<TResponse>(_serverEnvironment.ToUri(path), form);
        }
    }
}