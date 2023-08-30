using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Dto.Descriptions;

namespace ApiGateway.Client.Internal.Services
{
    internal class SolutionService : ISolutionService
    {
        private readonly Uri _uri;
        private readonly IWebClient _webClient;

        internal SolutionService(Uri uri, IWebClient webClient)
        {
            _uri = uri;
            _webClient = webClient;
        }

        public async Task<TaskDto> GetTask(string taskId)
        {
            var description = await _webClient.GetFromJsonAsync<TaskDescription>(_uri + $"tasks/{taskId}");
            var data = await _webClient.GetFromJsonAsync<TaskData>(_uri + $"player/tasks/{taskId}");
            return new TaskDto()
            {
                Description = description,
                Data = data
            };
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
    }
}