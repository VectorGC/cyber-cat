using System;
using System.Threading.Tasks;
using Shared.Models;

namespace ApiGateway.Client
{
    public interface IClient : IDisposable
    {
        void AddAuthorizationToken(string token);
        void RemoveAuthorizationToken();
        Task<string> Authenticate(string email, string password);
        Task<string> AuthorizePlayer(string token);
        Task<ITask> GetTask(string taskId);
        Task<string> GetSavedCode(string taskId);
        Task SaveCode(string taskId, string sourceCode);
        Task RemoveSavedCode(string taskId);
        Task<IVerdict> VerifySolution(string taskId, string sourceCode);
    }
}