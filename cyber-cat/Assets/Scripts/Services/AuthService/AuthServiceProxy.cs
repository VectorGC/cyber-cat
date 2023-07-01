using AuthService;
using Cysharp.Threading.Tasks;
using Models;
using ServerAPI;
using Services.InternalModels;

namespace Services.AuthService
{
    public class AuthServiceProxy : IAuthService
    {
        private readonly IServerAPI _serverAPI;

        public AuthServiceProxy(IServerAPI serverAPI)
        {
            _serverAPI = serverAPI;
        }

        public async UniTask<ITokenSession> Authenticate(string email, string password)
        {
            var token = await _serverAPI.Authenticate(email, password);
            return new TokenSession(token);
        }

        public async UniTask<IPlayer> AuthorizePlayer(ITokenSession token)
        {
            var name = await _serverAPI.AuthorizePlayer(token.Value);
            return new Player(name, token);
        }
    }
}