using System.Threading.Tasks;
using Shared.Models;

namespace ApiGateway.Client
{
    public interface IJudgeService
    {
        Task<IVerdict> VerifySolution(string taskId, string sourceCode);
    }

    public static class JudgeServiceFactory
    {
        public static IJudgeService Create(string uri, string token)
        {
            var client = new Client(uri);
            client.AddAuthorizationToken(token);

            return client;
        }
    }
}