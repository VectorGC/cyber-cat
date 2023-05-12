using System;
using Cysharp.Threading.Tasks;
using Models;
using Proyecto26;
using RestAPI.InternalDto;
using UnityEngine;

namespace RestAPI.RestAPIImplements
{
    public class RestAPI : IRestAPI
    {
        private readonly string _url;

        public RestAPI(string url)
        {
            _url = url;
        }

        public async UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = _url + "/authentication/login",
                Params =
                {
                    ["email"] = login,
                    ["password"] = password
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            var token = await RestClient.Get<TokenSessionDto>(request).ToUniTask();
            return token;
        }

        public UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token)
        {
            throw new NotImplementedException();
        }

        public UniTask<ITasks> GetTasks(IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}