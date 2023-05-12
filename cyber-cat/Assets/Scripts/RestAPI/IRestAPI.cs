using System;
using Cysharp.Threading.Tasks;
using Models;

namespace RestAPI
{
    public interface IRestAPI
    {
        UniTask<ITokenSession> Authenticate(string email, string password, IProgress<float> progress = null);
        UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token);
        UniTask<ITasks> GetTasks(IProgress<float> progress = null);
    }
}