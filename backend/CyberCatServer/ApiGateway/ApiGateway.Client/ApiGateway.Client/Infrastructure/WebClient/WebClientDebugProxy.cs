using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure.WebClient
{
    internal class WebClientDebugProxy : IWebClient
    {
        private readonly IWebClient _webClient;

        public WebClientDebugProxy(ServerEnvironment serverEnvironment, AuthorizationToken token = null)
        {
            _webClient = new WebClient(serverEnvironment, token);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }

        public async Task<string> GetAsync(string path)
        {
            try
            {
                return await _webClient.GetAsync(path);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string path)
        {
            try
            {
                return await _webClient.GetAsync<TResponse>(path);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public async Task<TResponse> GetFastJsonAsync<TResponse>(string path)
        {
            try
            {
                return await _webClient.GetFastJsonAsync<TResponse>(path);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public async Task<string> PostAsync(string path, Dictionary<string, string> form)
        {
            try
            {
                return await _webClient.PostAsync(path, form);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public async Task<string> PostAsync(string path)
        {
            try
            {
                return await _webClient.PostAsync(path);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public async Task<TResponse> PostAsync<TResponse>(string path, Dictionary<string, string> form)
        {
            try
            {
                return await _webClient.PostAsync<TResponse>(path, form);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public async Task<TResponse> PostFastJsonAsync<TResponse>(string path, Dictionary<string, string> form)
        {
            try
            {
                return await _webClient.PostFastJsonAsync<TResponse>(path, form);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public void AddHeader(string header, string value)
        {
            try
            {
                _webClient.AddHeader(header, value);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        public void RemoveHeader(string header)
        {
            try
            {
                _webClient.RemoveHeader(header);
            }
            catch (WebException e)
            {
                throw WebExceptionWithMessage(e);
            }
        }

        private WebException WebExceptionWithMessage(WebException exception)
        {
            var response = exception.Response as HttpWebResponse;
            if (response == null)
                return exception;

            var stream = response.GetResponseStream();
            var message = new StreamReader(stream).ReadToEnd();
            return new WebException(message, exception, exception.Status, exception.Response);
        }
    }
}