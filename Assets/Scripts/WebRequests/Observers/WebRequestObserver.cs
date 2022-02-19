using System;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace WebRequests.Observers
{
    public class WebRequestObserver : IObserver<UnityWebRequestAsyncOperation>
    {
        private event Action<string> _onResponse;

        private UnityWebRequestAsyncOperation _asyncOperation;

        public void OnResponse<T>(Action<T> callback)
        {
            OnResponse(SubscribeOnResponse);

            void SubscribeOnResponse(string responseText)
            {
                _onResponse -= SubscribeOnResponse;

                var obj = JsonConvert.DeserializeObject<T>(responseText);
                callback?.Invoke(obj);
            }
        }
        
        public void OnResponse(Action<string> callback)
        {
            void SubscribeOnResponse(string responseText)
            {
                _onResponse -= SubscribeOnResponse;
                callback?.Invoke(responseText);
            }

            _onResponse += SubscribeOnResponse;
        }

        public void OnCompleted()
        {
            _onResponse?.Invoke(_asyncOperation.webRequest.downloadHandler.text);
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(UnityWebRequestAsyncOperation asyncOperation)
        {
            _asyncOperation = asyncOperation;
        }
    }
}