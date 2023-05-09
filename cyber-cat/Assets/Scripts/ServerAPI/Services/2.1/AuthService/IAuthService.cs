using Authentication;

namespace AuthService
{
    public interface IAuthService
    {
        TokenSession Authenticate(string login, string password);
    }
}