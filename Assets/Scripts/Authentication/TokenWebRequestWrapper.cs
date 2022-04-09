using Cysharp.Threading.Tasks;
using RestAPIWrapper;

namespace Authentication
{
    public class TokenWebRequestWrapper : ITokenRequestWrapper
    {
        private readonly UniRx.Diagnostics.Logger _logger = new UniRx.Diagnostics.Logger(nameof(TokenWebRequestWrapper));
        
        public async UniTask<TokenSession> GetToken(string login, string password)
        {
            var token = await RestAPI.GetToken(login, password);
            ThrowIfTokenIsError(token);

            return token;
        }

        public async UniTask RegisterUser(string login, string password, string name)
        {
            var token = await RestAPI.RegisterUser(login, password, name);
            ThrowIfTokenIsError(token);
        }

        public async UniTask RestorePassword(string login, string password)
        {
            var token = await RestAPI.RestorePassword(login, password);
            ThrowIfTokenIsError(token);
        }

        private void ThrowIfTokenIsError(TokenSession token)
        {
            if (!string.IsNullOrEmpty(token.Error))
            {
                _logger.ThrowException(new TokenException(token.Error));
            }
        }
    }
}