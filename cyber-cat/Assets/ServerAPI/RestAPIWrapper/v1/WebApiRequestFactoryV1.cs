using Authentication;
using ServerAPIBase;

namespace RestAPIWrapper.V1
{
    public class WebApiRequestFactoryV1 : WebApiRequestFactory<string>
    {
        public override IAuthenticator<string> CreateAuthentificator()
        {
            return new AuthenticatorV1();
        }

        public override IPasswordRestorer<string> CreatePasswordRestorer()
        {
            return new PasswordRestorerV1();
        }

        public override IRegistrator<string> CreateRegistrator()
        {
            return new RegistratorV1();
        }

        public override ITokenReceiver<string> CreateTokenReceiver()
        {
            return new TokenReceiverV1();
        }

        public ITokenReceiver<TokenSession> CreateLocalTokenReceiver()
        {
            return new LocalTokenReceiverV1();
        }
    }
}