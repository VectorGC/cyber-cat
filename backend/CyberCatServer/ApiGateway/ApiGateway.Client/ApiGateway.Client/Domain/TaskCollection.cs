using System.Collections.Generic;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.Domain
{
    public class TaskCollection
    {
        public TaskModel this[TaskId taskId]
        {
            get => _tasks[taskId];
            set => _tasks[taskId] = value;
        }

        private readonly Dictionary<TaskId, TaskModel> _tasks = new Dictionary<TaskId, TaskModel>();

        public Dictionary<TaskId, TaskModel>.Enumerator GetEnumerator()
        {
            return _tasks.GetEnumerator();
        }

        public bool TryGetValue(TaskId taskId, out TaskModel taskModel)
        {
            return _tasks.TryGetValue(taskId, out taskModel);
        }
    }
}