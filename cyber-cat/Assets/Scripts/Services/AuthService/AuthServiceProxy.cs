using ApiGateway.Client;
using AuthService;
using Cysharp.Threading.Tasks;
using Models;
using Services.InternalModels;

namespace Services.AuthService
{
    public class AuthServiceProxy : IAuthService
    {
        private readonly IClient _client;

        public AuthServiceProxy(IClient client)
        {
            _client = client;
        }

        public async UniTask<ITokenSession> Authenticate(string email, string password)
        {
            var token = await _client.Authenticate(email, password);
            return new TokenSession(token);
        }

        public async UniTask<IPlayer> AuthorizePlayer(ITokenSession token)
        {
            var name = await _client.AuthorizePlayer(token.Value);
            return new Player(name, token);
        }
    }
}