using ApiGateway.Client.V3.Application.Services;
using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application.UseCases.Player
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

        public Result Execute(string password)
        {
            if (!_playerContext.IsLogined)
            {
                return Result.Failure("User not loggined");
            }

            var result = _userService.RemoveUserByToken(_playerContext.Token, password);
            if (result.IsSuccess)
                _playerContext.Clear();

            return result;
        }
    }
}