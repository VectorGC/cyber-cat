using DefaultNamespace.TestButtons;
using UnityEngine.Networking;

namespace Observers
{
    public static class UnityWebRequestExt
    {
        public static WebRequestObservable ToObservable(this UnityWebRequestAsyncOperation webRequestAsyncOperation) =>
            new WebRequestObservable(webRequestAsyncOperation);
    }
}