using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.Serverless
{
    public class AuthenticatorServerless : IAuthenticator<string>
    {
        public void Request(IAuthenticatorData data, Action<string> callback)
        {
            var token = new TokenSession();
            var json = JsonConvert.SerializeObject(token);
            callback?.Invoke(json);
        }

        public async UniTask<string> RequestAsync(IAuthenticatorData data, IProgress<float> progress = null)
        {
            var token = new TokenSession();
            return await UniTask.FromResult(JsonConvert.SerializeObject(token));
        }
    }
}

