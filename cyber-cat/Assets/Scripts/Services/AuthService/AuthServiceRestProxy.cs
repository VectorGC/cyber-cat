using AuthService;
using Cysharp.Threading.Tasks;

namespace Services.AuthService
{
    public class AuthServiceRestProxy : IAuthService
    {
        UniTask<ITokenSession> IAuthService.Authenticate(string login, string password)
        {
            /*
            new RestAPI()
            
            WebApiProxyV1 proxy = new WebApiProxyV1();
            TokenSession token = new TokenSession();
            proxy.Authentificate(login, password, login, null, x => token = x);
            return token;
            */
            throw new System.NotImplementedException();
        }
    }
}