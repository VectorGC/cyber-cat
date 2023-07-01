using System;
using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public interface IClient : IAuthorizationService, ITaskRepository, IJudgeService, IDisposable
    {
        void AddAuthorizationToken(string token);
        void RemoveAuthorizationToken();
        Task<string> GetSavedCode(string taskId);
        Task SaveCode(string taskId, string sourceCode);
        Task RemoveSavedCode(string taskId);
    }
}