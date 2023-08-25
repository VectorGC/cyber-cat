using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Authorization;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ApiGateway.Client.Internal
{
    internal class AuthorizedClient : IAuthorizedClient, ITaskRepository, ISolutionService, IJudgeService, IPlayerService
    {
        public ITaskRepository Tasks => this;
        public ISolutionService SolutionService => this;
        public IJudgeService JudgeService => this;
        public IPlayerService PlayerService => this;

        private readonly IWebClient _webClient;
        private readonly Uri _uri;

        public AuthorizedClient(Uri uri, string token, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
            _webClient.AddAuthorizationHeader(JwtBearerDefaults.AuthenticationScheme, token);
        }

        public async Task<TaskDto> GetTask(string taskId)
        {
            var task = await _webClient.GetFromJsonAsync<TaskDto>(_uri + $"tasks/{taskId}");
            return task;
        }

        public async Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            return await _webClient.PostAsJsonAsync<VerdictDto>(_uri + $"judge/verify/{taskId}", new Dictionary<string, string>()
            {
                ["sourceCode"] = sourceCode
            });
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
        
        public async Task CreatePlayer()
        {
            await _webClient.PostAsync(_uri + "player/create");
        }

        public async Task RemovePlayer()
        {
            await _webClient.DeleteAsync(_uri + "player/delete");
        }

        public async Task<PlayerDto> GetPlayer()
        {
            var player = await _webClient.GetFromJsonAsync<PlayerDto>(_uri + "player/get");
            return player;
        }

        public async Task AddBitcoins(int bitcoins)
        {
            await _webClient.PutAsync(_uri + "player/bitcoins/add", new Dictionary<string, string>
            {
                ["bitcoins"] = bitcoins.ToString()
            });
        }

        public async Task RemoveBitcoins(int bitcoins)
        {
            await _webClient.PutAsync(_uri + "player/bitcoins/remove", new Dictionary<string, string>
            {
                ["bitcoins"] = bitcoins.ToString()
            });
        }
    }
}