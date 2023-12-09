using ApiGateway.Client.Application.Services;

namespace ApiGateway.Client.Application.UseCases.Player
{
    public class RemoveCurrentPlayer : IUseCase
    {
        private readonly IUserService _userService;
        private readonly PlayerContext _playerContext;

        public RemoveCurrentPlayer(IUserService userService, PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _userService = userService;
        }

        public Result Execute()
        {
            if (!_playerContext.IsLogined)
                return Result.Failure(ErrorCode.NotLoggined);

            var result = _userService.RemoveUserByToken(_playerContext.Token);
            if (result.IsSuccess)
                _playerContext.Clear();

            return result;
        }
    }
}