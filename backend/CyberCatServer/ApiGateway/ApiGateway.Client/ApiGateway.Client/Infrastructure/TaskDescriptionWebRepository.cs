using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Infrastructure
{
    public class TaskDescriptionWebRepository : ITaskDescriptionRepository
    {
        private readonly WebClientFactory _webClientFactory;
        private Dictionary<TaskId, TaskDescription> _tasks;

        public TaskDescriptionWebRepository(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<TaskDescription> GetTaskDescription(TaskId taskId)
        {
            var tasks = await GetAllTaskDescriptions();
            return tasks[taskId];
        }

        public async Task<Dictionary<TaskId, TaskDescription>> GetAllTaskDescriptions()
        {
            if (_tasks != null)
                return _tasks;

            using (var client = _webClientFactory.Create())
            {
                var descriptions = await client.GetAsync<List<TaskDescription>>(WebApi.GetTaskDescriptions);
                var dict = descriptions.ToDictionary(kvp => new TaskId(kvp.Id), kvp => kvp);

                _tasks = dict;
                return dict;
            }
        }

        public async Task<List<TestCaseDescription>> GetTestCases(TaskId taskId)
        {
            using (var client = _webClientFactory.Create())
            {
                var descriptions = await client.GetFastJsonAsync<List<TestCaseDescription>>(WebApi.GetTestCases(taskId));
                return descriptions;
            }
        }
    }
}