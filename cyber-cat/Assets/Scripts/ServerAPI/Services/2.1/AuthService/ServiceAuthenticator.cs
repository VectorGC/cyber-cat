using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto26;
using ServerAPIBase;
using System;

namespace AuthService
{
    public class ServiceAuthenticator : IAuthenticator<TokenSession>
    {
        public void Request(IAuthenticatorData data, Action<TokenSession> callback)
        {
            SendRequest(data).Done(x => DoneAuth(x, callback));
        }

        public async UniTask<TokenSession> RequestAsync(IAuthenticatorData data, IProgress<float> progress = null)
        {
            var response = await SendRequest(data).ToUniTask();
            return JsonConvert.DeserializeObject<TokenSession>(response.Text);
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

        private void DoneAuth(ResponseHelper response, Action<TokenSession> callback)
        {
            callback?.Invoke(JsonConvert.DeserializeObject<TokenSession>(response.Text));
        }
    }
}