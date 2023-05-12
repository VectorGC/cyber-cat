using System;
using Cysharp.Threading.Tasks;
using Models;

namespace RestAPI.RestAPIImplements
{
    public class RestAPI : IRestAPI
    {
        public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public UniTask<ITasks> GetTasks(IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token)
        {
            throw new NotImplementedException();
        }
    }
}