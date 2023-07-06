using System.Threading.Tasks;
using Shared.Models.Models;

namespace ApiGateway.Client.Services
{
    public interface IJudgeService
    {
        Task<IVerdict> VerifySolution(string taskId, string sourceCode);
    }
}