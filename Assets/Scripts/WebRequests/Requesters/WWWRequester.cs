using System;
using UniRx;
using UnityEngine;
using WebRequests.Extensions;

namespace WebRequests.Requesters
{
    public class WWWRequester
    {
        private static TimeSpan Timeout => TimeSpan.FromSeconds(5);

        public IObservable<string> SendGetRequest(IWebRequest webRequest, IProgress<float> progress = null)
        {
            var url = webRequest.GetUri().ToString();

            Debug.Log($"Send get request to '{url}'");
            var observable = ObservableWWW.Get(url, progress: progress).Timeout(Timeout);
#if DEBUG
            observable.Subscribe(Debug.Log);
#endif

            return observable;
        }
    }
}