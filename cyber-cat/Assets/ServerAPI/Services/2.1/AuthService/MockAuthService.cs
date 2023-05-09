using Authentication;
using RestAPIWrapper.Serverless;

namespace AuthService
{
    public class MockAuthService : IAuthService
    {
        public TokenSession Authenticate(string login, string password)
        {
            WebAPIProxyServerless proxy = new WebAPIProxyServerless();
            TokenSession token = new TokenSession();
            proxy.Authentificate(login, password, login, null, x => token = x);
            return token;
        }
    }
}