using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application.UseCases.Player
{
    public class LogoutPlayer : IUseCase
    {
        private readonly PlayerContext _playerContext;

        public LogoutPlayer(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public Result Execute()
        {
            if (!_playerContext.IsLogined)
            {
                return Result.Failure("User forbidden");
            }

            _playerContext.Clear();
            return Result.Success;
        }
    }
}