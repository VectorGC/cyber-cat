using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;
using ApiGateway.Client.Services.Authorization;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ApiGateway.Client.Internal
{
    internal class Client : IAuthorizationService, ITaskRepository, IJudgeService, ISolutionService, IPlayerService, IDisposable
    {
        private readonly Uri _uri;
        private readonly IWebClient _webClient;

        public Client(string uri, IWebClient webClient)
        {
            _uri = new Uri(uri);
            _webClient = webClient;
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }

        public async Task<string> GetAuthenticationToken(string email, string password)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = email,
                ["password"] = password
            };

            var accessToken = await _webClient.PostAsync(_uri + "auth/login", form);
            return accessToken;
        }

        public async Task<TaskDto> GetTask(string taskId)
        {
            var task = await _webClient.GetFromJsonAsync<TaskDto>(_uri + $"tasks/{taskId}");
            return task;
        }

        public void AddAuthorizationToken(string token)
        {
            _webClient.AddAuthorizationHeader(JwtBearerDefaults.AuthenticationScheme, token);
        }

        public async Task<string> GetSavedCode(string taskId)
        {
            return await _webClient.GetStringAsync(_uri + $"solution/{taskId}");
        }

        public async Task SaveCode(string taskId, string sourceCode)
        {
            await _webClient.PostAsync(_uri + $"solution/{taskId}", new Dictionary<string, string>()
            {
                ["sourceCode"] = sourceCode
            });
        }

        public async Task RemoveSavedCode(string taskId)
        {
            await _webClient.DeleteAsync(_uri + $"solution/{taskId}");
        }

        public async Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            return await _webClient.PostAsJsonAsync<VerdictDto>(_uri + $"judge/verify/{taskId}", new Dictionary<string, string>()
            {
                ["sourceCode"] = sourceCode
            });
        }

        public async Task AddNewPlayer()
        {
            await _webClient.PostAsync(_uri + "player/create");
        }

        public async Task DeletePlayer()
        {
            await _webClient.DeleteAsync(_uri + "player/delete");
        }

        public async Task<PlayerDto> GetPlayerById()
        {
            var player = await _webClient.GetFromJsonAsync<PlayerDto>(_uri + "player/get");
            return player;
        }

        public async Task AddBitcoinsToPlayer(int bitcoins)
        {
            await _webClient.PutAsync(_uri + $"player/bitcoins/add", new Dictionary<string, string>
            {
                ["bitcoins"] = bitcoins.ToString()
            });
        }

        public async Task TakeBitcoinsFromPlayer(int bitcoins)
        {
            await _webClient.PutAsync(_uri + $"player/bitcoins/take", new Dictionary<string, string>
            {
                ["bitcoins"] = bitcoins.ToString()
            });
        }
    }
}