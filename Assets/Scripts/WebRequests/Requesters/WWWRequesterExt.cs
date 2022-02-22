using System;
using Observers;
using UniRx;

namespace WebRequests.Requesters
{
    public static class WWWRequesterExt
    {
        public static IObservable<string> SendWWWGet(this IWebRequest webRequest)
        {
            var requester = new WWWRequester();
            return requester.SendGetRequest(webRequest);
        }

        public static IObservable<T> SendWWWGetObject<T>(this IWebRequest webRequest)
        {
            return webRequest.SendWWWGet().JsonDeserialize<T>();
        }

        public static IDisposable SendWWWGetObject<T>(this IWebRequest webRequest, Action<T> onNext)
        {
            return webRequest.SendWWWGetObject<T>().Subscribe(onNext);
        }
    }
}