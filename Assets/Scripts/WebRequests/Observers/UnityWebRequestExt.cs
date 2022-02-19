using UnityEngine.Networking;

namespace Observers.WebRequest
{
    public static class UnityWebRequestExt
    {
        public static IWebRequestObservable ToObservable(this UnityWebRequest webRequest) =>
            new WebRequestObservable(webRequest);
    }
}