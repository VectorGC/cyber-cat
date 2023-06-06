using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto;

namespace ApiGateway.Client
{
    public class Client : IClient, IDisposable
    {
        private readonly Uri _uri;
        private readonly IRestClient _restClient;

        public Client(string uri, IRestClient restClient)
        {
            _uri = new Uri(uri);
            _restClient = restClient;
        }

        public void Dispose()
        {
            _restClient.Dispose();
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password
            };

            var accessToken = await _restClient.PostAsync(_uri + "auth/login", form);
            return accessToken;
        }

        public async Task<string> AuthorizePlayer(string token)
        {
            AddAuthorizationToken(token);
            var name = await _restClient.GetStringAsync(_uri + "auth/authorize_player");
            RemoveAuthorizationToken();

            return name;
        }

        public async Task<TaskDto> GetTask(string taskId)
        {
            var task = await _restClient.GetFromJsonAsync<TaskDto>(_uri + $"tasks/{taskId}");
            return task;
        }

        public void AddAuthorizationToken(string token)
        {
            _restClient.AddAuthorizationHeader(JwtBearerDefaults.AuthenticationScheme, token);
        }

        public void RemoveAuthorizationToken()
        {
            _restClient.RemoveAuthorizationHeader();
        }

        public async Task<string> GetSavedCode(string taskId)
        {
            return await _restClient.GetStringAsync(_uri + $"solution/{taskId}");
        }

        public async Task SaveCode(string taskId, string sourceCode)
        {
            await _restClient.PostStringAsync(_uri + $"solution/{taskId}", sourceCode);
        }

        public async Task RemoveSavedCode(string taskId)
        {
            await _restClient.DeleteAsync(_uri + $"solution/{taskId}");
        }

        public async Task<VerdictDto> VerifySolution(string taskId, string sourceCode)
        {
            return await _restClient.PostAsJsonAsync<VerdictDto>(_uri + $"judge/verify/{taskId}", sourceCode);
        }
    }
}