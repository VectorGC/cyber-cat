using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using UnityEngine;

namespace Authentication
{
    public struct AuthTokenRequestWrapper : IAuthTokenRequestWrapper
    {
        public async UniTask<TokenSession> GetAuthData(string login, string password,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = RestAPIWrapper.Endpoint.LOGIN,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            return await RestClient.Get<TokenSession>(request).ToUniTask();
        }

        public async UniTask RegisterUser(string login, string password, string name)
        {
            var request = new RequestHelper
            {
                Uri = RestAPIWrapper.Endpoint.REGISTER,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password,
                    ["name"] = name
                },
                EnableDebug = Debug.isDebugBuild
            };

            await RestClient.Post(request).ToUniTask();
        }

        public async UniTask RestorePassword(string login, string password)
        {
            var request = new RequestHelper
            {
                Uri = RestAPIWrapper.Endpoint.RESTORE,
                Params =
                {
                    ["email"] = login,
                    ["pass"] = password,
                },
                EnableDebug = Debug.isDebugBuild
            };

            await RestClient.Post(request).ToUniTask();
        }
    }
}