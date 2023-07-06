using System.Threading.Tasks;
using ApiGateway.Client.Services;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ApiGateway.Client.Internal.ServerlessServices
{
    internal class JudgeServiceServerless : IJudgeService
    {
        public Task<IVerdict> VerifySolution(string taskId, string sourceCode)
        {
            var verdict = new VerdictDto
            {
                Status = VerdictStatus.Success
            };
            return Task.FromResult<IVerdict>(verdict);
        }
    }
}