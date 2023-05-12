using Cysharp.Threading.Tasks;
using Models;

namespace AuthService
{
    public interface IAuthService
    {
        UniTask<ITokenSession> Authenticate(string login, string password);
        UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token);
    }
}