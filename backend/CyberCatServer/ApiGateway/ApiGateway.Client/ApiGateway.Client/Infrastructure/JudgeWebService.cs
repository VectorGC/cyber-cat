using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Infrastructure
{
    public class JudgeWebService : IJudgeService
    {
        private readonly WebClientFactory _webClientFactory;

        public JudgeWebService(WebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;
        }

        public async Task<Verdict> GetVerdict(TaskId taskId, string solution)
        {
            using (var webClient = _webClientFactory.Create())
            {
                var verdict = await webClient.PostFastJsonAsync<Verdict>(WebApi.JudgeGetVerdict, new Dictionary<string, string>()
                {
                    ["taskId"] = taskId.Value,
                    ["solution"] = solution
                });

                return verdict;
            }
        }
    }
}