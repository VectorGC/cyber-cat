using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Ids;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.V3.Infrastructure
{
    public class SubmitSolutionTaskService
    {
        private readonly WebClientFactory _webClientFactory;

        public SubmitSolutionTaskService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<Verdict> VerifySolution(TaskId taskId, string solution, AuthorizationToken token)
        {
            using (var client = _webClientFactory.Create(token))
            {
                var verdict = await client.PostAsync<Verdict>(WebApi.SubmitSolution(taskId), new Dictionary<string, string>()
                {
                    ["solution"] = solution
                });

                return verdict;
            }
        }
    }
}