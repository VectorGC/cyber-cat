using AuthService;
using Cysharp.Threading.Tasks;
using Models;
using RestAPI;

namespace Services.AuthService
{
    public class AuthServiceRestProxy : IAuthService
    {
        private readonly IRestAPI _restAPI;

        public AuthServiceRestProxy(IRestAPI restAPI)
        {
            _restAPI = restAPI;
        }

        public async UniTask<ITokenSession> Authenticate(string email, string password)
        {
            return await _restAPI.Authenticate(email, password);
        }

        public async UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token)
        {
            return await _restAPI.AuthorizeAsPlayer(token);
        }
    }
}