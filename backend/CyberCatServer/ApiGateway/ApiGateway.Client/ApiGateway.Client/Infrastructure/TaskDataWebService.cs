using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure
{
    public class TaskDataWebService : ITaskDataService
    {
        private readonly WebClientFactory _webClientFactory;

        public TaskDataWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<IReadOnlyList<UserModel>> GetUsersWhoSolvedTask(TaskId taskId, AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var result = await client.GetAsync<List<UserModel>>(WebApi.GetUsersWhoSolvedTask(taskId));
                return result;
            }
        }
    }
}