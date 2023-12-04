using ApiGateway.Client.Application.Services;

namespace ApiGateway.Client.Application.UseCases.Vk
{
    public class RemoveCurrentPlayerWithVk : IUseCase
    {
        private readonly IUserService _userService;
        private readonly PlayerContext _playerContext;

        public RemoveCurrentPlayerWithVk(IUserService userService, PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _userService = userService;
        }

        public Result Execute(string vkId)
        {
            if (!_playerContext.IsLogined)
            {
                return Result.Failure("User not loggined");
            }

            var result = _userService.RemoveUserWithVk(_playerContext.Token, vkId);
            if (result.IsSuccess)
                _playerContext.Clear();

            return result;
        }
    }
}