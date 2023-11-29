using System;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application
{
    public class PlayerService : IDisposable
    {
        public event Func<PlayerContext, Task> OnPlayerLogined;

        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly PlayerContext _playerContext;

        public PlayerService(PlayerContext playerContext, AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
            _playerContext = playerContext;
        }

        public void Dispose()
        {
            if (_playerContext.IsLogined)
            {
                Logout();
            }

            _playerContext.Clear();
        }

        public async Task<Result<PlayerModel>> LoginPlayer(string email, string password)
        {
            var result = await _authService.LoginPlayer(email, password);
            if (!result.IsSuccess)
            {
                return result.Error;
            }

            var user = await _userService.GetUserByToken(result.Value);
            _playerContext.SetContext(user, result.Value);

            await OnPlayerLogined?.Invoke(_playerContext);

            return _playerContext.Player;
        }

        public async Task<Result> RegisterPlayer(string email, string password, string userName)
        {
            var result = await _authService.RegisterPlayer(email, password, userName);
            if (!result.IsSuccess)
            {
                return Result.Failure(result.Error);
            }

            return Result.Success;
        }

        public Result Remove()
        {
            if (!_playerContext.IsLogined)
            {
                return Result.Failure("User forbidden");
            }

            var result = _userService.RemoveUserByToken(_playerContext.Token);
            if (!result.IsSuccess)
            {
                return Result.Failure(result.Error);
            }

            Logout();
            return Result.Success;
        }

        public Result Logout()
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