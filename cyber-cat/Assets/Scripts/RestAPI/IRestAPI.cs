using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;

namespace RestAPI
{
    public interface IRestAPI
    {
        UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null);
        UniTask<ITasks> GetTasks(IProgress<float> progress = null);
        UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token);
    }
}