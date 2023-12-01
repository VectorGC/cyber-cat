using ApiGateway.Client.V3.Application.UseCases.Player;
using ApiGateway.Client.V3.Infrastructure;
using Shared.Models.Domain.Users;

namespace ApiGateway.Client.V3.Application.API
{
    public class PlayerAPI : API
    {
        public TasksAPI Tasks { get; }
        public UserModel User => _playerContext.Player.User;

        private readonly PlayerContext _playerContext;

        public PlayerAPI(TinyIoCContainer container, TasksAPI tasksApi, PlayerContext playerContext) : base(container)
        {
            _playerContext = playerContext;
            Tasks = tasksApi;
        }

        public Result Remove(string password) => GetUseCase<RemoveCurrentPlayer>().Execute(password);
        public Result Logout() => GetUseCase<LogoutPlayer>().Execute();
    }
}