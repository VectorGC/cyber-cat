using Authentication;
using ServerAPIBase;

namespace AuthService
{
    public class MockAuthService : IAuthService
    {
        public TokenSession Authenticate(string login, string password)
        {
            TokenSession token = new TokenSession();
            IAuthenticatorData data = new AuthenticatorData(login, password, login);
            new MockServiceAuthenticator().Request(data, x => token = x);
            return token;
        }
    }
}