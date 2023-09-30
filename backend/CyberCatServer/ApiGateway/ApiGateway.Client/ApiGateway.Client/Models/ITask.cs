using System.Threading.Tasks;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<string> GetDefaultCode();
        Task<ITaskProgressStatus> GetStatus();
        Task<IVerdict> VerifySolution(string sourceCode);
    }
}