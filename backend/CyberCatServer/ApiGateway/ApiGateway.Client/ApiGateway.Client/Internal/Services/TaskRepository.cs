using System;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Services;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Dto.Descriptions;

namespace ApiGateway.Client.Internal.Services
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly Uri _uri;
        private readonly IWebClient _webClient;

        internal TaskRepository(Uri uri, IWebClient webClient)
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
    }
}