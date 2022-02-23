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

        public static IObservable<TResponse> SendWWWGetObject<TResponse>(this IGetWebRequest<TResponse> webRequest)
        {
            return webRequest.SendWWWGet().JsonDeserialize<TResponse>();
        }

        public static IDisposable SendWWWGetObject<TResponse>(this IGetWebRequest<TResponse> webRequest,
            Action<TResponse> onNext)
        {
            return webRequest.SendWWWGetObject<TResponse>().Subscribe(onNext);
        }
    }
}