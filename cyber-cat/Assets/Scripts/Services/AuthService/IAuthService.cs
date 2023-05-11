using Cysharp.Threading.Tasks;

namespace AuthService
{
    public interface IAuthService
    {
        UniTask<ITokenSession> Authenticate(string login, string password);
    }
}