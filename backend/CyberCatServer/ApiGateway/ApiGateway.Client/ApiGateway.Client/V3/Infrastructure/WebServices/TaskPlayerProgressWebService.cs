using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure.WebServices
{
    public class TaskPlayerProgressWebService : ITaskPlayerProgressService
    {
        private readonly WebClientFactory _webClientFactory;

        public TaskPlayerProgressWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<TaskProgress> GetTaskProgress(TaskId taskId, AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var progress = await client.GetAsync<TaskProgress>(WebApi.GetTaskProgress(taskId.Value));
                return progress;
            }
        }

        public async Task<List<TaskProgress>> GetTasksProgress(AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var progressData = await client.GetAsync<List<TaskProgress>>(WebApi.GetTasksProgress);
                return progressData;
            }
        }
    }
}