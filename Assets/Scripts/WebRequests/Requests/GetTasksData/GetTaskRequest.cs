using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Authentication;
using WebRequests.Extensions;

namespace WebRequests.Requests.GetTasksData
{
    public interface ISendRequestHandler : IWebRequest
    {
        void SendRequest();
    }

    public class GetTaskRequest : IWebReturnedResponse<TaskData>, ISendRequestHandler
    {
        private readonly string _taskId;
        private readonly TokenSession _token;
        
        private event Action<TaskData> _onResponse;

        public GetTaskRequest OnResponse(Action<TaskData> callback)
        {
            _onResponse += callback;
            return this;
        }

        public void SendRequest()
        {
            new GetTasksRequest(_token)
                .OnResponse(tasksData =>
                {
                    var taskData = tasksData.Tasks[_taskId];
                    _onResponse?.Invoke(taskData);
                    _onResponse = null;
                })
                .SendRequest();
        }

        public NameValueCollection QueryParams => _token.ToQueryParam();

        public GetTaskRequest(string taskId) : this(taskId, TokenSession.FromPlayerPrefs())
        {
        }

        public GetTaskRequest(string taskId, TokenSession token)
        {
            _taskId = taskId;
            _token = token;
        }
    }
}