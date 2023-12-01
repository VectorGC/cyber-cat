using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application.UseCases.Player
{
    public class LoginPlayer : IUseCase
    {
        private readonly IUserService _userService;
        private readonly PlayerContext _playerContext;
        private readonly PlayerModelFactory _playerModelFactory;

        public LoginPlayer(PlayerContext playerContext, IUserService userService, PlayerModelFactory playerModelFactory)
        {
            _playerModelFactory = playerModelFactory;
            _userService = userService;
            _playerContext = playerContext;
        }

        public async Task<Result<PlayerModel>> Execute(string email, string password)
        {
            var result = await _userService.LoginUser(email, password);
            if (!result.IsSuccess)
            {
                return Result<PlayerModel>.Failure(result.Error);
            }

            var player = await _playerModelFactory.Create(result.Value);
            _playerContext.SetContext(player, result.Value);

            return _playerContext.Player;
        }
    }
}