using System.Collections.Specialized;
using Authentication;

namespace WebRequests
{
    public class GetTasksRequest : IWebRequest
    {
        public NameValueCollection QueryParams => TokenSession.AsQueryParam();
    }
}