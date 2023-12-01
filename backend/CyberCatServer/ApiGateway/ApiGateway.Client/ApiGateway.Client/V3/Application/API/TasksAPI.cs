using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.V3.Application.API
{
    public class TasksAPI : API
    {
        public TaskAPI this[TaskId taskId] => new TaskAPI(_playerContext.Player.Tasks[taskId], this);

        private readonly PlayerContext _playerContext;

        public TasksAPI(TinyIoCContainer container, PlayerContext playerContext) : base(container)
        {
            _playerContext = playerContext;
        }
    }
}