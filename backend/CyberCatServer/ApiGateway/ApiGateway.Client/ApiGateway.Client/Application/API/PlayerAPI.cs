using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;
using ApiGateway.Client.Application.CQRS.Commands;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.API
{
    public class PlayerAPI
    {
        public TasksAPI Tasks { get; }
        public UserModel User => _playerContext.Player.User;
        public bool IsLogginedWithVk => _playerContext.Token is VkAccessToken;

        private readonly PlayerContext _playerContext;
        private readonly Mediator _mediator;

        public PlayerAPI(Mediator mediator, TasksAPI tasksApi, PlayerContext playerContext)
        {
            _mediator = mediator;
            _playerContext = playerContext;
            Tasks = tasksApi;
        }

        public async Task<Result> Logout()
        {
            var command = new LogoutPlayer();
            var result = await _mediator.SendSafe(command);
            return Result.FromObject(result);
        }

        public async Task<Result> Remove()
        {
            var command = new RemoveCurrentPlayer();
            var result = await _mediator.SendSafe(command);
            return Result.FromObject(result);
        }
    }
}