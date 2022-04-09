using Cysharp.Threading.Tasks;

namespace Authentication
{
    public interface ITokenRequestWrapper
    {
        UniTask<TokenSession> GetToken(string login, string password);
    }
}