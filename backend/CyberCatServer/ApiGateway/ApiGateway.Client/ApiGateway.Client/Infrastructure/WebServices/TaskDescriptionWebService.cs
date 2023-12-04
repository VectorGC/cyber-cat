using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure.WebServices
{
    public class TaskDescriptionWebService : ITaskDescriptionService
    {
        private readonly WebClientFactory _webClientFactory;

        public TaskDescriptionWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<List<TaskDescription>> GetTaskDescriptions(AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var descriptions = await client.GetAsync<List<TaskDescription>>(WebApi.GetTaskDescriptions);
                return descriptions;
            }
        }

        public async Task<List<TestCaseDescription>> GetTestCaseDescriptions(TaskId taskId, AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var descriptions = await client.GetFastJsonAsync<List<TestCaseDescription>>(WebApi.GetTestCases(taskId));
                return descriptions;
            }
        }
    }
}