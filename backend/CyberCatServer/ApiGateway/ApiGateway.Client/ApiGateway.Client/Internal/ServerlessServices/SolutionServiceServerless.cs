using System.Threading.Tasks;
using ApiGateway.Client.Services;

namespace ApiGateway.Client.Internal.ServerlessServices
{
    internal class SolutionServiceServerless : ISolutionService
    {
        public Task<string> GetSavedCode(string taskId)
        {
            return Task.FromResult("Hello World!");
        }

        public Task SaveCode(string taskId, string sourceCode)
        {
            return Task.CompletedTask;
        }

        public Task RemoveSavedCode(string taskId)
        {
            return Task.CompletedTask;
        }
    }
}