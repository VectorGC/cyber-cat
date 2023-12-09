using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;
using ApiGateway.Client.Domain;

namespace ApiGateway.Client.Application.UseCases.Vk
{
    public class LoginWithVk : IUseCase
    {
        private readonly IUserService _userService;
        private readonly PlayerContext _playerContext;
        private readonly PlayerModelFactory _playerModelFactory;

        public LoginWithVk(PlayerContext playerContext, IUserService userService, PlayerModelFactory playerModelFactory)
        {
            _playerModelFactory = playerModelFactory;
            _userService = userService;
            _playerContext = playerContext;
        }

        public async Task<Result<PlayerModel>> Execute(string email, string userName, string vkId)
        {
            if (_playerContext.IsLogined)
            {
                return Result<PlayerModel>.Failure(ErrorCode.AlreadyLoggined);
            }

            var result = await _userService.LoginWithVk(email, userName, vkId);
            if (!result.IsSuccess)
            {
                return Result<PlayerModel>.Failure(result);
            }

            var player = await _playerModelFactory.Create(result.Value);
            _playerContext.SetContext(player, result.Value);

            return _playerContext.Player;
        }
    }
}