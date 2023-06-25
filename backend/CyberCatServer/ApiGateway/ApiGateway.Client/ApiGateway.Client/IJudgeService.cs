using System.Threading.Tasks;
using Shared.Models;
using Shared.Models.Models;

namespace ApiGateway.Client
{
    public interface IJudgeService
    {
        Task<IVerdict> VerifySolution(string taskId, string sourceCode);
    }

    public static class JudgeServiceFactory
    {
        public static IJudgeService Create(string uri, string token, IRestClient restClient)
        {
            var client = new Client(uri, restClient);
            client.AddAuthorizationToken(token);

            return client;
        }
    }
}