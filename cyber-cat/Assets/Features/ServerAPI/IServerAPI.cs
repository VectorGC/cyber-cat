using Cysharp.Threading.Tasks;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace ServerAPI
{
    public interface IServerAPI
    {
        UniTask<TaskDto> GetTask(string taskId);
        UniTask<string> Authenticate(string email, string password);
        UniTask<string> AuthorizePlayer(string token);
        void AddAuthorizationToken(string token);
        UniTask<string> GetSavedCode(string taskId);
        UniTask<IVerdict> VerifySolution(string taskId, string sourceCode);
    }
}