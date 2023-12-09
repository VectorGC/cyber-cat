using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Application.UseCases.Player;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.API
{
    public class PlayerAPI : UseCaseAPI
    {
        public TasksAPI Tasks { get; }
        public UserModel User => _playerContext.Player.User;
        public bool IsLogginedWithVk => _playerContext.Token is VkAccessToken;

        private readonly PlayerContext _playerContext;

        public PlayerAPI(TinyIoCContainer container, TasksAPI tasksApi, PlayerContext playerContext) : base(container)
        {
            _playerContext = playerContext;
            Tasks = tasksApi;
        }

        public Result Logout() => GetUseCase<LogoutPlayer>().Execute();
        public Result Remove() => GetUseCase<RemoveCurrentPlayer>().Execute();
    }
}