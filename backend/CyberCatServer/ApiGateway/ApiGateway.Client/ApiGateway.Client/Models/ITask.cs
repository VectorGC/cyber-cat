using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models.Ids;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<ITaskProgressStatus> GetStatus();
        Task<SortedDictionary<TestCaseId, ITestCase>> GetTestCases();
        Task<IVerdict> VerifySolution(string sourceCode);
    }
}