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
        private readonly ApiGatewayClient _client;

        public PlayerAPI(Mediator mediator, TasksAPI tasksApi, PlayerContext playerContext, ApiGatewayClient client)
        {
            _client = client;
            _playerContext = playerContext;
            Tasks = tasksApi;
        }

        public async Task<Result> Logout()
        {
            return await _client.Logout();
        }

        public async Task<Result> Remove()
        {
            return await _client.Remove();
        }
    }
}