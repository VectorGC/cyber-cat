using System.Collections.Specialized;
using UnityEngine.Networking;

namespace WebRequests
{
    public interface IWebRequest
    {
        NameValueCollection QueryParams { get; }
    }

    public interface IUnityWebRequestHandler
    {
        UnityWebRequest GetWebRequestHandler(string uri);
    }

    public interface IWebReturnedResponse<TResponse> : IWebRequest
    {
    }
}