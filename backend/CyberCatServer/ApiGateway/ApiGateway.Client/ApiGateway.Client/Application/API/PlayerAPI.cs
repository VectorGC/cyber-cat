using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Application.UseCases.Player;
using Shared.Models.Domain.Users;

namespace ApiGateway.Client.Application.API
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