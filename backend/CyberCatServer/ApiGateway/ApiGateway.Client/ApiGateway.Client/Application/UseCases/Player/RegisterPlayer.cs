using System.Threading.Tasks;
using ApiGateway.Client.Application.Services;

namespace ApiGateway.Client.Application.UseCases.Player
{
    public class RegisterPlayer : IUseCase
    {
        private readonly IUserService _userService;

        public RegisterPlayer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Execute(string email, string password, string userName)
        {
            if (string.IsNullOrEmpty(email))
                return Result.Failure(ErrorCode.EmailEmpty);

            if (string.IsNullOrEmpty(password))
                return Result.Failure(ErrorCode.PasswordEmpty);

            if (string.IsNullOrEmpty(userName))
                return Result.Failure(ErrorCode.UserNameEmpty);

            return await _userService.RegisterPlayer(email, password, userName);
        }
    }
}