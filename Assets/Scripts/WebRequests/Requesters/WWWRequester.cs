using System;
using UniRx;
using WebRequests.Extensions;

namespace WebRequests.Requesters
{
    public class WWWRequester
    {
        public IObservable<string> SendGetRequest(IWebRequest webRequest)
        {
            var url = webRequest.GetUri().ToString();
            return ObservableWWW.Get(url);
        }
    }
}