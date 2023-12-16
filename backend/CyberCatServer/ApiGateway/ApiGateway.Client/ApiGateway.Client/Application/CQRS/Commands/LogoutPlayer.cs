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
        private readonly IVerdictHistory _verdictHistory;

        public LogoutPlayerHandler(PlayerContext playerContext, IVerdictHistory verdictHistory)
        {
            _verdictHistory = verdictHistory;
            _playerContext = playerContext;
        }

        public Task Handle(LogoutPlayer command)
        {
            _playerContext.Clear();
            _verdictHistory.Clear();
            return Task.CompletedTask;
        }
    }
}