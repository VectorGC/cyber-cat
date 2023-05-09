using Authentication;
using ServerAPIBase;

namespace AuthService
{
    public class AuthService : IAuthService
    {
        public TokenSession Authenticate(string login, string password)
        {
            TokenSession token = new TokenSession();
            IAuthenticatorData data = new AuthenticatorData(login, password, login);
            new ServiceAuthenticator().Request(data, x => token = x);
            return token;
        }
    }
}