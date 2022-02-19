using UnityEngine.Networking;

namespace WebRequests.Observers
{
    public static class UnityWebRequestExt
    {
        public static IWebRequestObservable ToObservable(this UnityWebRequest webRequest) =>
            new WebRequestObservable(webRequest);
    }
}