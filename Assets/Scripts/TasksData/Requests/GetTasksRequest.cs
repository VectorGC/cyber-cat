using System.Collections.Specialized;
using Authentication;
using WebRequests.Requesters;

namespace TasksData.Requests
{
    public class GetTasksRequest : IGetWebRequest<TasksData>
    {
        private readonly TokenSession _token;

        public NameValueCollection QueryParams => _token.ToQueryParam();

        public GetTasksRequest() : this(TokenSession.FromPlayerPrefs())
        {
        }

        public GetTasksRequest(TokenSession token)
        {
            _token = token;
        }
    }
}