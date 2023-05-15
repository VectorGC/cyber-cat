using AuthService;
using Cysharp.Threading.Tasks;
using Models;
using ServerAPI;

namespace Services.AuthService
{
    public class AuthServiceProxy : IAuthService
    {
        private readonly IServerAPI serverAPI;

        public AuthServiceProxy(IServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
        }

        public async UniTask<ITokenSession> Authenticate(string email, string password)
        {
            return await serverAPI.Authenticate(email, password);
        }

        public async UniTask<IPlayer> AuthorizePlayer(ITokenSession token)
        {
            return await serverAPI.AuthorizePlayer(token);
        }
    }
}