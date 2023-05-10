using Authentication;
using RestAPIWrapper.V1;

namespace AuthService
{
    public class AuthService : IAuthService
    {
        public TokenSession Authenticate(string login, string password)
        {
            WebApiProxyV1 proxy = new WebApiProxyV1();
            TokenSession token = new TokenSession();
            proxy.Authentificate(login, password, login, null, x => token = x);
            return token;
        }
    }
}