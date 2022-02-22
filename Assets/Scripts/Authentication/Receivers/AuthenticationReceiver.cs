using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Authentication.Receivers
{
    [CreateAssetMenu(fileName = "AuthenticationReceiver", menuName = "ScriptableObjects/Authentication/Receiver")]
    public class AuthenticationReceiver : ScriptableObject//, IAuthenticationReceiver
    {
        private UnityWebRequest _webRequest;

        // public void Subscribe(IWebRequestObservable webRequestObservable)
        // {
        //     _webRequest = webRequestObservable.WebRequest;
        //     webRequestObservable.Subscribe(this);
        // }

        public void OnCompleted()
        {
            var textResult = _webRequest.downloadHandler.text;
            
            var tokenSession = TokenSession.FromJson(textResult);
            tokenSession.SaveToPlayerPrefs();
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(UnityWebRequestAsyncOperation value)
        {
        }
    }
}