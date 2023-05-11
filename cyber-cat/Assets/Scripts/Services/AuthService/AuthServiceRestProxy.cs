using AuthService;
using Cysharp.Threading.Tasks;
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

        public async UniTask<ITokenSession> Authenticate(string login, string password)
        {
            return await _restAPI.Authenticate(login, password);
        }
    }
}