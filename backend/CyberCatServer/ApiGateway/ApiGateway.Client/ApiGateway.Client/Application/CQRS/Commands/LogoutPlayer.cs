using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class LogoutPlayer : ICommand, IAuthorizedOnly
    {
    }

    public class LogoutPlayerHandler : ICommandHandler<LogoutPlayer>
    {
        private readonly PlayerContext _playerContext;
        private readonly IVerdictHistoryService _verdictHistoryService;

        public LogoutPlayerHandler(PlayerContext playerContext, IVerdictHistoryService verdictHistoryService)
        {
            _verdictHistoryService = verdictHistoryService;
            _playerContext = playerContext;
        }

        public Task Handle(LogoutPlayer command)
        {
            _playerContext.Clear();
            _verdictHistoryService.Clear();
            return Task.CompletedTask;
        }
    }
}