using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;

namespace ServerAPI
{
    public interface IServerAPI
    {
        UniTask<ITask> GetTask(string taskId);
        UniTask<string> Authenticate(string email, string password);
        UniTask<string> AuthorizePlayer(string token);
        void AddAuthorizationToken(string token);
    }
}