using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Infrastructure;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class LoginPlayerWithVk : ICommand, IAnonymousOnly
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string VkId { get; set; }
    }

    public class LoginPlayerWithVkHandler : ICommandHandler<LoginPlayerWithVk>
    {
        private readonly WebClientFactory _webClientFactory;
        private readonly PlayerContext _playerContext;
        private readonly PlayerModelFactory _playerModelFactory;

        public LoginPlayerWithVkHandler(WebClientFactory webClientFactory, PlayerContext playerContext, PlayerModelFactory playerModelFactory)
        {
            _playerModelFactory = playerModelFactory;
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
        }

        public async Task Handle(LoginPlayerWithVk command)
        {
            var form = new Dictionary<string, string>
            {
                ["email"] = command.Email,
                ["userName"] = command.UserName,
                ["vkId"] = command.VkId
            };

            using (var client = _webClientFactory.Create())
            {
                var accessToken = await client.PostFastJsonAsync<VkAccessToken>(WebApi.LoginWithVk, form);
                var player = await _playerModelFactory.Create(accessToken);
                _playerContext.SetContext(player, accessToken);
            }
        }
    }
}