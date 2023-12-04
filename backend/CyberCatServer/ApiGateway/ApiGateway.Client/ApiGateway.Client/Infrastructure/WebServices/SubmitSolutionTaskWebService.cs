using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure.WebServices
{
    public class SubmitSolutionTaskWebService : ISubmitSolutionTaskService
    {
        private readonly WebClientFactory _webClientFactory;

        public SubmitSolutionTaskWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<Verdict> SubmitSolution(TaskId taskId, string solution, AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var verdict = await client.PostFastJsonAsync<Verdict>(WebApi.SubmitSolution(taskId), new Dictionary<string, string>()
                {
                    ["solution"] = solution
                });

                return verdict;
            }
        }
    }
}