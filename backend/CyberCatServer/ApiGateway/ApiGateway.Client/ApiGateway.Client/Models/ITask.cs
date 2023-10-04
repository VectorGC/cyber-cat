using System.Threading.Tasks;
using Shared.Models.Models.TestCases;
using Shared.Models.Models.Verdicts;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<ITaskProgressStatus> GetStatus();
        Task<TestCases> GetTestCases();
        Task<Verdict> VerifySolution(string sourceCode);
    }
}