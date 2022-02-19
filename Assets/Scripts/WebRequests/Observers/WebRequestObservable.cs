using System;
using System.Net;
using UnityEngine.Networking;

namespace Observers.WebRequest
{
    public class WebRequestObservable : AsyncOperationObservable<UnityWebRequestAsyncOperation>,
        IWebRequestObservable
    {
        public UnityWebRequest WebRequest { get; }

        public WebRequestObservable(UnityWebRequest webRequest) : base(webRequest.SendWebRequest())
        {
            WebRequest = webRequest;
        }

        protected override void OnCompleted(UnityWebRequestAsyncOperation asyncOperation,
            IObserver<UnityWebRequestAsyncOperation> observer)
        {
            switch (asyncOperation.webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    observer.OnCompleted();
                    break;
                default:
                    observer.OnError(new WebException(asyncOperation.webRequest.error));
                    break;
            }
        }
    }
}