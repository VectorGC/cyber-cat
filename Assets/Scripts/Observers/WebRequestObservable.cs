using System;
using System.Net;
using UnityEngine.Networking;

namespace Observers
{
    public class WebRequestObservable : AsyncOperationObservable<UnityWebRequestAsyncOperation>
    {
        public WebRequestObservable(UnityWebRequestAsyncOperation asyncOperation) : base(asyncOperation)
        {
        }

        protected override void CallCompleted(UnityWebRequestAsyncOperation asyncOperation,
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