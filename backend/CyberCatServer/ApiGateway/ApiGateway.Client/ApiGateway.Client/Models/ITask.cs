using System.Threading.Tasks;

namespace ApiGateway.Client.Models
{
    public interface ITask
    {
        Task<string> GetName();
        Task<string> GetDescription();
        Task<ITaskProgressStatus> GetStatus();
        Task<IVerdict> VerifySolution(string sourceCode);
    }
}