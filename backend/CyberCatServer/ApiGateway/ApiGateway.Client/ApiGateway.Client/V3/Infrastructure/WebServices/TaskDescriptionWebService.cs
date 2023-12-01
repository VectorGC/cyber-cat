using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure.WebServices
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
    }
}