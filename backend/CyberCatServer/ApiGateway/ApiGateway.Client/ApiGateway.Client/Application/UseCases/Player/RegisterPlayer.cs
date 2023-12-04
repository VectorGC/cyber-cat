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

        public Task<Result> Execute(string email, string password, string userName)
        {
            return _userService.RegisterPlayer(email, password, userName);
        }
    }
}