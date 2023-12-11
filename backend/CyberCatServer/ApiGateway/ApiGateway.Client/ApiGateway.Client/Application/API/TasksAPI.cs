using System.Collections.Generic;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.Application.API
{
    public class TasksAPI
    {
        public TaskAPI this[TaskId taskId] => _tasks[taskId];

        private readonly Dictionary<TaskId, TaskAPI> _tasks = new Dictionary<TaskId, TaskAPI>();

        public TasksAPI(PlayerContext playerContext, Mediator mediator)
        {
            foreach (var kvp in playerContext.Player.Tasks)
            {
                _tasks[kvp.Key] = new TaskAPI(kvp.Value, mediator, playerContext);
            }
        }
    }
}