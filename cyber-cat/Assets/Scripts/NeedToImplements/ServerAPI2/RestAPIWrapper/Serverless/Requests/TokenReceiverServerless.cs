using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.Serverless
{
    public class TokenReceiverServerless : ITokenReceiver<string>
    {
        public void Request(ITokenReceiverData data, Action<string> callback)
        {
            var token = new TokenSession();
            var json = JsonConvert.SerializeObject(token);
            callback?.Invoke(json);
        }

        public async UniTask<string> RequestAsync(ITokenReceiverData data, IProgress<float> progress = null)
        {
            var token = new TokenSession();
            return await UniTask.FromResult(JsonConvert.SerializeObject(token));
        }
    }
}