using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Application.UseCases.Player;
using ApiGateway.Client.Application.UseCases.Vk;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.API
{
    public class PlayerAPI : API
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

        public Result Remove(string password) => GetUseCase<RemoveCurrentPlayer>().Execute(password);
        public Result Logout() => GetUseCase<LogoutPlayer>().Execute();
        public Result RemoveWithVk(string vkId) => GetUseCase<RemoveCurrentPlayerWithVk>().Execute(vkId);
    }
}