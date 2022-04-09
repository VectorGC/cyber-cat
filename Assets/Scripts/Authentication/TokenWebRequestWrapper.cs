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
            if (!string.IsNullOrEmpty(token.Error))
            {
                _logger.ThrowException(new RequestTokenException(token.Error));
            }

            return token;
        }
    }
}