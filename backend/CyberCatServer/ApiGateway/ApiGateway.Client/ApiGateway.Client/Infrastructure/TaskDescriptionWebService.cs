using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Infrastructure
{
    public class TaskDescriptionWebService : ITaskDescriptionService
    {
        private readonly WebClientFactory _webClientFactory;

        public TaskDescriptionWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<Result<Dictionary<TaskId, TaskDescription>>> GetTaskDescriptions()
        {
            try
            {
                using (var client = _webClientFactory.Create())
                {
                    var descriptions = await client.GetAsync<List<TaskDescription>>(WebApi.GetTaskDescriptions);
                    var dict = descriptions.ToDictionary(kvp => new TaskId(kvp.Id), kvp => kvp);
                    return dict;
                }
            }
            catch (WebException webException) when (webException.Response is HttpWebResponse httpWebResponse)
            {
                throw;
            }
        }

        public async Task<List<TestCaseDescription>> GetTestCaseDescriptions(TaskId taskId)
        {
            using (var client = _webClientFactory.Create())
            {
                var descriptions = await client.GetFastJsonAsync<List<TestCaseDescription>>(WebApi.GetTestCases(taskId));
                return descriptions;
            }
        }
    }
}