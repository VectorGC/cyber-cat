using System.Threading.Tasks;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<ITaskProgressStatus> GetStatus();
        Task<TestCases> GetTestCases();
        Task<IVerdict> VerifySolution(string sourceCode);
        Task<IVerdictV2> VerifySolutionV2(string sourceCode);
    }
}