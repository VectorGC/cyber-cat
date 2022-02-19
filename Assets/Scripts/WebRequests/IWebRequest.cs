using System.Collections.Specialized;

namespace WebRequests
{
    public interface IWebRequest
    {
        NameValueCollection QueryParams { get; }
    }
}