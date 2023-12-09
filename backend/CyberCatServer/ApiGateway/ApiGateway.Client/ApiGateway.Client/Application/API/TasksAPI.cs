using System.Collections.Generic;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.Application.API
{
    public class TasksAPI : UseCaseAPI
    {
        public TaskAPI this[TaskId taskId] => _tasks[taskId];

        private readonly Dictionary<TaskId, TaskAPI> _tasks = new Dictionary<TaskId, TaskAPI>();

        public TasksAPI(TinyIoCContainer container, PlayerContext playerContext) : base(container)
        {
            // TODO:
            foreach (var kvp in playerContext.Player.Tasks)
            {
                _tasks[kvp.Key] = new TaskAPI(kvp.Value, this);
            }
        }
    }
}