using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Infrastructure.WebClient;
using Shared.Models.Infrastructure;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class RemoveCurrentPlayer : ICommand, IAuthorizedOnly
    {
    }

    public class RemoveCurrentPlayerHandler : ICommandHandler<RemoveCurrentPlayer>
    {
        private readonly WebClientFactory _webClientFactory;
        private readonly PlayerContext _playerContext;

        public RemoveCurrentPlayerHandler(WebClientFactory webClientFactory, PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
        }

        public async Task Handle(RemoveCurrentPlayer command)
        {
            using (var client = _webClientFactory.Create(_playerContext.Token))
            {
                await client.PostAsync(WebApi.RemoveUser, new Dictionary<string, string>());
                _playerContext.Clear();
            }
        }
    }
}