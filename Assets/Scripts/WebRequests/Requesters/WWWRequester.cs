using System;
using UniRx;
using UnityEngine;
using WebRequests.Extensions;

namespace WebRequests.Requesters
{
    public class WWWRequester
    {
        public IObservable<string> SendGetRequest(IWebRequest webRequest)
        {
            var url = webRequest.GetUri().ToString();

            Debug.Log($"Send get request to '{url}'");
            var observable = ObservableWWW.Get(url);
#if DEBUG
            observable.Subscribe(Debug.Log);
#endif

            return observable;
        }
    }
}