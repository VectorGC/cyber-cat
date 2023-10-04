using System.Threading.Tasks;
using Shared.Models.Models;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<ITaskProgressStatus> GetStatus();
        Task<TestCases> GetTestCases();
        Task<IVerdict> VerifySolution(string sourceCode);
        Task<VerdictV2> VerifySolutionV2(string sourceCode);
    }
}