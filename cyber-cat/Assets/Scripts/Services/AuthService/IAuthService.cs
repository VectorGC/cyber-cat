using Cysharp.Threading.Tasks;
using Models;

namespace AuthService
{
    public interface IAuthService
    {
        UniTask<ITokenSession> Authenticate(string email, string password);
        UniTask<IPlayer> AuthorizePlayer(ITokenSession token);
    }
}