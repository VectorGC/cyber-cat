using Cysharp.Threading.Tasks;
using Proyecto26;
using ServerAPIBase;
using UnityEngine;
using System;

namespace RestAPIWrapper.V1
{
    public class TokenReceiverV1 : ITokenReceiver<string>
    {
        public void Request(ITokenReceiverData data, Action<string> callback)
        {
            SendRequest(data).Done(x => DoneAuth(x, callback));
        }

        public async UniTask<string> RequestAsync(ITokenReceiverData data, IProgress<float> progress = null)
        {
            var response = await SendRequest(data).ToUniTask();
            return response.Text;
        }

        private RSG.IPromise<ResponseHelper> SendRequest(ITokenReceiverData data, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = ServerData.URL + "/authentication/login",
                Params =
                {
                    ["email"] = data.Email,
                    ["password"] = data.Password
                },
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };
            return RestClient.Get(request);
        }

        private void DoneAuth(ResponseHelper response, Action<string> callback)
        {
            callback?.Invoke(response.Text);
        }
    }
}