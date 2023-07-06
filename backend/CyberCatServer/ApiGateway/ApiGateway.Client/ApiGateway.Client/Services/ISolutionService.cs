using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public interface ISolutionService
    {
        Task<string> GetSavedCode(string taskId);
        Task SaveCode(string taskId, string sourceCode);
        Task RemoveSavedCode(string taskId);
    }
}