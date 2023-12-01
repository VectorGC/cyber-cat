using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.Services;
using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application.UseCases.Player
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