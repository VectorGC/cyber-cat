using System;
using UnityEngine.Networking;
using WebRequests.Observers;

namespace WebRequests
{
    public static class WebRequestExt
    {
        private const string DEFAULT_DOMAIN = "https://kee-reel.com/cyber-cat";

        public static void SendGetRequest<TResponse>(this IWebRequest webRequest, Action<TResponse> onResponse,
            string domain = DEFAULT_DOMAIN)
        {
            var url = webRequest.GetUrl();
            var unityWebRequest = UnityWebRequest.Get(url);

            var observable = unityWebRequest.ToObservable();

            var observer = new WebRequestObserver();
            observer.OnResponse<TResponse>(responseObj => { onResponse?.Invoke(responseObj); });

            observable.Subscribe(observer);
        }

        public static IWebRequestObservable SendGetRequest(this IWebRequest webRequest, string domain = DEFAULT_DOMAIN)
        {
            var url = webRequest.GetUrl();
            var unityWebRequest = UnityWebRequest.Get(url);
            return unityWebRequest.ToObservable();
        }

        public static string GetUrl(this IWebRequest webRequest, string domain = DEFAULT_DOMAIN)
        {
            var builder = new UriBuilder(domain)
            {
                Query = webRequest.QueryParams.ToString()
            };

            return builder.ToString();
        }
    }
}