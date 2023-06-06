using System;
using System.Threading.Tasks;
using Shared.Dto;

namespace ApiGateway.Client
{
    public interface IClient : IDisposable
    {
        void AddAuthorizationToken(string token);
        void RemoveAuthorizationToken();
        Task<string> Authenticate(string email, string password);
        Task<string> AuthorizePlayer(string token);
        Task<TaskDto> GetTask(string taskId);
        Task<string> GetSavedCode(string taskId);
        Task SaveCode(string taskId, string sourceCode);
        Task RemoveSavedCode(string taskId);
        Task<VerdictDto> VerifySolution(string taskId, string sourceCode);
    }
}