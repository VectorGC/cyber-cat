using UnityEngine.Networking;

namespace Observers
{
    public static class UnityWebRequestExt
    {
        public static WebRequestObservable ToObservable(this UnityWebRequest webRequest) =>
            new WebRequestObservable(webRequest);
    }
}