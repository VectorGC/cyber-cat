using System.Collections;
using System.Collections.Generic;
using ApiGateway.Client.Models;
using Shared.Models.Ids;

namespace ApiGateway.Client.Internal.Serverless
{
    internal class TaskRepositoryServerless : ITaskRepository
    {
        private readonly Dictionary<TaskId, ITask> _tasks = new Dictionary<TaskId, ITask>();

        private static TaskRepositoryServerless _instance;

        public static TaskRepositoryServerless GetOrCreate()
        {
            if (_instance == null)
            {
                _instance = new TaskRepositoryServerless();
            }

            return _instance;
        }

        private TaskRepositoryServerless()
        {
            _tasks["tutorial"] = new TaskServerless();
            _tasks["task-1"] = new TaskServerless();
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