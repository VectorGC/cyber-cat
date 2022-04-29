using Cysharp.Threading.Tasks;

namespace Authentication
{
    public interface ITokenRequestWrapper
    {
        UniTask<TokenSession> GetToken(string login, string password);
        UniTask RegisterUser(string login, string password, string name);
        UniTask RestorePassword(string login, string password);
    }
}