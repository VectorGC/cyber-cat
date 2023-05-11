using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;

namespace RestAPI
{
    public class RestAPI : IRestAPI
    {
        public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }

        public Task<ITasks> GetTasks(IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}