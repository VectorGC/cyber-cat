using System;
using Authentication;
using Extensions.DotNetExt;
using UniRx;
using WebRequests;

namespace TasksData.Requests
{
    public class GetTaskRequest : ISendRequestHandler<ITaskTicket>
    {
        private readonly string _taskId;
        private readonly TokenSession _token;

        public GetTaskRequest(string taskId) : this(taskId, TokenSession.FromPlayerPrefs())
        {
        }

        public GetTaskRequest(string taskId, TokenSession token)
        {
            _taskId = taskId;
            _token = token;
        }

        public IObservable<ITaskTicket> SendRequest() =>
            new GetTasksRequest(_token)
                .SendRequest()
                .Select(tasks => tasks.Tasks.GetValue(_taskId));
    }
}