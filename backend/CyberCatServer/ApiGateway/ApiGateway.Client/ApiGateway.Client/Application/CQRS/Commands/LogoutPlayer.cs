using System.Threading.Tasks;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class LogoutPlayer : ICommand, IAuthorizedOnly
    {
    }

    public class LogoutPlayerHandler : ICommandHandler<LogoutPlayer>
    {
        private readonly PlayerContext _playerContext;

        public LogoutPlayerHandler(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public Task Handle(LogoutPlayer command)
        {
            _playerContext.Clear();
            return Task.CompletedTask;
        }
    }
}