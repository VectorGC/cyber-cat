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

        public async Task<string> Authenticate(string email, string password, IProgress<float> progress = null)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password
            };

            var accessToken = await _restClient.PostAsync(_uri + "auth/login", form);
            return accessToken;
        }

        public async Task<string> AuthorizePlayer(string token, IProgress<float> progress = null)
        {
            AddAuthorizationToken(token);
            var name = await _restClient.GetStringAsync(_uri + "auth/authorize_player");

            return name;
        }

        public async Task<TaskDto> GetTask(string taskId, IProgress<float> progress = null)
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
    }
}