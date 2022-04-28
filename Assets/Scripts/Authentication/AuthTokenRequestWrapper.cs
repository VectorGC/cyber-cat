using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using RestAPIWrapper;
using UnityEngine;

namespace Authentication
{
    public struct AuthTokenRequestWrapper : IAuthTokenRequestWrapper
    {
        private const string Login = Endpoint.ROOT + "/login";
        private const string Register = Endpoint.ROOT + "/register";
        private const string Restore = Endpoint.ROOT + "/restore";

        public async UniTask<TokenSession> GetAuthData(string login, string password,
            IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = Login,
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
                Uri = Register,
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
                Uri = Restore,
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