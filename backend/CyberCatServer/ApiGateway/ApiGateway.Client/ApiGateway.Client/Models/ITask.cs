using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using Shared.Models.Domain.Verdicts;
using Shared.Models.TO_REMOVE;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<string> GetDefaultCode();
        Task<ITaskProgressStatus> GetStatus();
        Task<TestCases> GetTestCases();
        Task<Verdict> VerifySolution(string sourceCode);
    }
}