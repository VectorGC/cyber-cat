using Cysharp.Threading.Tasks;
using Proyecto26;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.V1
{
    public class AuthenticatorV1 : IAuthenticator<string>
    {
        public void Request(IAuthenticatorData data, Action<string> callback)
        {
            SendRequest(data).Done(x => DoneAuth(x, callback));
        }

        public async UniTask<string> RequestAsync(IAuthenticatorData data, IProgress<float> progress = null)
        {
            var response = await SendRequest(data).ToUniTask();
            return response.Text;
        }

        private RSG.IPromise<ResponseHelper> SendRequest(IAuthenticatorData data, IProgress<float> progress = null)
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
                EnableDebug = ServerData.DebugBuild
            };
            return RestClient.Get(request);
        }

        private void DoneAuth(ResponseHelper response, Action<string> callback)
        {
            callback?.Invoke(response.Text);
        }
    }
}