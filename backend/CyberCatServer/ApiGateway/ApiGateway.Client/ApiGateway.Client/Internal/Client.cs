using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;
using ApiGateway.Client.Services.Authorization;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ApiGateway.Client.Internal
{
    internal class Client : IAuthorizationService, ITaskRepository, IJudgeService, ISolutionService, IDisposable
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

        public void RemoveAuthorizationToken()
        {
            _webClient.RemoveAuthorizationHeader();
        }

        public async Task<string> GetSavedCode(string taskId)
        {
            return await _webClient.GetStringAsync(_uri + $"solution/{taskId}");
        }

        public async Task SaveCode(string taskId, string sourceCode)
        {
            await _webClient.PostStringAsync(_uri + $"solution/{taskId}", sourceCode);
        }

        public async Task RemoveSavedCode(string taskId)
        {
            await _webClient.DeleteAsync(_uri + $"solution/{taskId}");
        }

        public async Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            return await _webClient.PostAsJsonAsync<VerdictDto>(_uri + $"judge/verify/{taskId}", sourceCode);
        }
    }
}