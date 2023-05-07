using Authentication;
using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;
using UnityEngine;

namespace RestAPIWrapper.V1
{
    public class LocalTokenReceiverV1 : ITokenReceiver<TokenSession>
    {
        public void Request(ITokenReceiverData data, Action<TokenSession> callback)
        {
            var tokenSession = GetTokenSession();
            callback?.Invoke(tokenSession);
        }

        public async UniTask<TokenSession> RequestAsync(ITokenReceiverData data, IProgress<float> progress = null)
        {
            var tokenSession = GetTokenSession();
            return tokenSession;
        }

        private TokenSession GetTokenSession()
        {
            var token = PlayerPrefs.GetString(PlayerPrefsInfo.Key);
            var name = PlayerPrefs.GetString(PlayerPrefsInfo.Name);
            return new TokenSession(token, name);
        }
    }
}