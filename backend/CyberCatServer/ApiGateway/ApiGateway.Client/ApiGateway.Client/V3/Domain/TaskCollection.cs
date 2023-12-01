using System.Collections.Generic;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.V3.Domain
{
    public class TaskCollection
    {
        public TaskModel this[TaskId taskId]
        {
            get => _tasks[taskId];
            set => _tasks[taskId] = value;
        }

        private readonly Dictionary<TaskId, TaskModel> _tasks = new Dictionary<TaskId, TaskModel>();
    }
}