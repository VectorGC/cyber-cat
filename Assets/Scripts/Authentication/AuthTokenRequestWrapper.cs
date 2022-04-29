using System;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;

namespace Authentication
{
    public struct AuthTokenRequestWrapper : IAuthTokenRequestWrapper
    {
        public async UniTask<string> GetAuthData(string login, string password, IProgress<float> progress = null)
        {
            return await RestAPI.Instance.GetAuthData(login, password, progress);
        }

        public async UniTask RegisterUser(string login, string password, string name)
        {
            await RestAPI.Instance.RegisterUser(login, password, name);
        }

        public async UniTask RestorePassword(string login, string password)
        {
            await RestAPI.Instance.RestorePassword(login, password);
        }
    }
}