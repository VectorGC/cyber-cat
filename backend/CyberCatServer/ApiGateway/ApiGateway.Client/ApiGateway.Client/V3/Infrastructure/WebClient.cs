using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    internal class WebClient : IDisposable
    {
        public ServerEnvironment ServerEnvironment { get; }

        private readonly IWebClientAdapter _webClient;
        private readonly AuthorizationToken _token;

        public WebClient(ServerEnvironment serverEnvironment, AuthorizationToken token = null)
        {
            _webClient =
#if UNITY_WEBGL
            new UnityWebRequest.UnityWebClient();
#endif
#if WEB_CLIENT
                new ApiGateway.Client.Internal.WebClientAdapters.WebClientAdapter.WebClientAdapter();
#endif

            _token = token;
            ServerEnvironment = serverEnvironment;

            if (token != null)
                _webClient.AddAuthorizationHeader(token.Type, token.Value);
        }

        public void Dispose()
        {
            _webClient.RemoveAuthorizationHeader();
            _webClient.Dispose();
        }

        public Task<string> GetAsync(string path)
        {
            return _webClient.GetStringAsync(ServerEnvironment.ToUri(path));
        }

        public Task<TResponse> GetFastJsonAsync<TResponse>(string path)
        {
            return _webClient.GetFromFastJsonPolymorphicAsync<TResponse>(ServerEnvironment.ToUri(path));
        }

        public Task<TResponse> GetAsync<TResponse>(string path)
        {
            return _webClient.GetFromJsonAsync<TResponse>(ServerEnvironment.ToUri(path));
        }

        public Task<string> PostAsync(string path, Dictionary<string, string> form)
        {
            return _webClient.PostAsync(ServerEnvironment.ToUri(path), form);
        }

        public Task<string> PostAsync(string path)
        {
            return _webClient.PostAsync(ServerEnvironment.ToUri(path));
        }

        public async Task<TResponse> PostFastJsonAsync<TResponse>(string path, Dictionary<string, string> form)
        {
            return await _webClient.PostAsFastJsonPolymorphicAsync<TResponse>(ServerEnvironment.ToUri(path), form);
        }
        
        public async Task<TResponse> PostAsync<TResponse>(string path, Dictionary<string, string> form)
        {
            return await _webClient.PostAsFastJsonPolymorphicAsync<TResponse>(ServerEnvironment.ToUri(path), form);
        }
    }
}