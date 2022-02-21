using System;
using System.Collections.Specialized;
using Authentication;
using ServerData;
using WebRequests.Extensions;

namespace WebRequests.Requests.GetTasksData
{
    public class GetTasksRequest : IWebReturnedResponse<TasksData>
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

        public void SendRequest(Action<TasksData> onResponse) => this.OnResponse(onResponse).SendRequest();
    }
}