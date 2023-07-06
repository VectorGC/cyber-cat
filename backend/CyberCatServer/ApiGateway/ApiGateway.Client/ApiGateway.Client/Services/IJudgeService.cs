using System.Threading.Tasks;
using Shared.Models.Models;

namespace ApiGateway.Client
{
    public interface IJudgeService
    {
        Task<IVerdict> VerifySolution(string taskId, string sourceCode);
    }
}