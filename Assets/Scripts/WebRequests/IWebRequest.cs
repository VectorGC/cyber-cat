using System.Collections.Specialized;
using UnityEngine.Networking;

namespace WebRequests
{
    public interface IWebRequest
    {
        NameValueCollection QueryParams { get; }
    }
}