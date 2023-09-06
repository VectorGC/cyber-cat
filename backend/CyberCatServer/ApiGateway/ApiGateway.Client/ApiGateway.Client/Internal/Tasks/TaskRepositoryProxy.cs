using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters;
using ApiGateway.Client.Models;
using Shared.Models.Dto;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;

namespace ApiGateway.Client.Internal.Tasks
{
    internal class TaskRepositoryProxy : ITaskRepository
    {
        private readonly Uri _uri;
        private readonly IWebClient _webClient;
        private readonly Dictionary<TaskId, ITask> _tasks = new Dictionary<TaskId, ITask>();

        public TaskRepositoryProxy(Uri uri, IWebClient webClient)
        {
            _webClient = webClient;
            _uri = uri;
        }

        public async Task Init()
        {
            var tasks = await _webClient.GetFromJsonAsync<TaskIdsDto>(_uri + "tasks");
            foreach (var taskId in tasks.taskIds)
            {
                var taskProxy = new TaskProxy(taskId, _uri, _webClient);
                _tasks[taskId] = taskProxy;
            }
        }

        #region | Delegate implementation |

        public ITask this[TaskId taskId] => _tasks[taskId];
        public IEnumerable<TaskId> Keys => _tasks.Keys;
        public IEnumerable<ITask> Values => _tasks.Values;
        public int Count => _tasks.Count;

        public bool ContainsKey(TaskId key)
        {
            return _tasks.ContainsKey(key);
        }

        public bool TryGetValue(TaskId key, out ITask value)
        {
            return _tasks.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<TaskId, ITask>> GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _tasks).GetEnumerator();
        }

        #endregion | Delegate implementation |
    }
}