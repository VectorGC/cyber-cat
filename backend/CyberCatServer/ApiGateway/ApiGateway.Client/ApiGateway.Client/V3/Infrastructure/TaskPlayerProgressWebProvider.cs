using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Tasks;
using Shared.Models.Ids;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class TaskPlayerProgressWebProvider
    {
        private readonly WebClientFactory _webClientFactory;

        public TaskPlayerProgressWebProvider(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<TaskProgressData> GetTaskProgress(TaskId taskId, AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var progressData = await client.GetAsync<TaskProgressData>(WebApi.GetTaskProgressData(taskId));
                return progressData;
            }
        }

        public async Task<List<TaskProgressData>> GetTasksProgress(AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var progressData = await client.GetAsync<List<TaskProgressData>>(WebApi.GetTasksProgressData);
                return progressData;
            }
        }
    }
}