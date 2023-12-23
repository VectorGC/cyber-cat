using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
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
        private readonly IVerdictHistoryService _verdictHistoryService;

        public RemoveCurrentPlayerHandler(WebClientFactory webClientFactory, PlayerContext playerContext, IVerdictHistoryService verdictHistoryService)
        {
            _verdictHistoryService = verdictHistoryService;
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
        }

        public async Task Handle(RemoveCurrentPlayer command)
        {
            using (var client = _webClientFactory.Create(_playerContext.Token))
            {
                await client.PostAsync(WebApi.RemoveUser, new Dictionary<string, string>());
                _playerContext.Clear();
                _verdictHistoryService.Clear();
            }
        }
        
        public async Task Handle(object command)
        {
            await Handle(command as RemoveCurrentPlayer);
        }
    }
}