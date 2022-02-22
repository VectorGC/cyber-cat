using System;
using System.Collections.Specialized;
using Authentication;
using WebRequests;
using WebRequests.Extensions;
using WebRequests.Requesters;

namespace TasksData.Requests
{
    public class GetTasksRequest : IWebRequest, ISendRequestHandler<TasksData>
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
        
        public IObservable<TasksData> SendRequest() => this.SendWWWGetObject<TasksData>();
    }
}