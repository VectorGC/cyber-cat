using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace WebRequests.Observers
{
    public class WebResponseTextObserver : IObserver<UnityWebRequestAsyncOperation>
    {
        public void OnCompleted()
        {
            var t = 10;
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(UnityWebRequestAsyncOperation value)
        {
        }
    }
    public class WebRequestObserver : IObserver<UnityWebRequestAsyncOperation>
    {
        private event Action<string> _onResponse;

        private UnityWebRequestAsyncOperation _asyncOperation;

        public void OnResponse<T>(Action<T> callback)
        {
            OnResponse(OnResponsed);

            void OnResponsed(string responseText)
            {
                _onResponse -= OnResponsed;

                try
                {
                    var obj = JsonConvert.DeserializeObject<T>(responseText);
                    callback?.Invoke(obj);
                }
                catch (JsonReaderException)
                {
                    Debug.LogError($"Error on deserialize response text: '{responseText}'");
                    throw;
                }
            }
        }

        public void OnResponse(Action<string> callback)
        {
            void OnResponsed(string responseText)
            {
                _onResponse -= OnResponsed;
                callback?.Invoke(responseText);
            }

            _onResponse += OnResponsed;
        }

        public void OnCompleted()
        {
            _onResponse?.Invoke(_asyncOperation.webRequest.downloadHandler.text);
        }

        public void OnError(Exception error)
        {
            Debug.LogError(error);
        }

        public void OnNext(UnityWebRequestAsyncOperation asyncOperation)
        {
            _asyncOperation = asyncOperation;
        }
    }
}