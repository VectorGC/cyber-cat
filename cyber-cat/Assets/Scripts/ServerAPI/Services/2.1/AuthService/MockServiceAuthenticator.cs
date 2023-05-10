using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using ServerAPIBase;
using System;

namespace AuthService
{
    public class MockServiceAuthenticator : IAuthenticator<TokenSession>
    {
        public void Request(IAuthenticatorData data, Action<TokenSession> callback)
        {
            var token = new TokenSession("serverless_token", "Cyber Cat");
            callback?.Invoke(token);
        }

        public UniTask<TokenSession> RequestAsync(IAuthenticatorData data, IProgress<float> progress = null)
        {
            var token = new TokenSession("serverless_token", "Cyber Cat");
            return UniTask.FromResult(token);
        }
    }
}