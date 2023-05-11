using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Models;

namespace RestAPI
{
    public interface IRestAPI
    {
        UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null);
        Task<ITasks> GetTasks(IProgress<float> progress = null);
    }
}