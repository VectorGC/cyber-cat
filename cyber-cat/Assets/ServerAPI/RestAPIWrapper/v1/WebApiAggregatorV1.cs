using ServerAPIBase;

namespace RestAPIWrapper.V1
{
    public class WebApiAggregatorV1 : IWebApiAggregator<string>
    {
        public IRegistrator<string> Registrator { get; }
        public IAuthenticator<string> Authenticator { get; }
        public IPasswordRestorer<string> PasswordRestorer { get; }
        public ITokenReceiver<string> TokenReceiver { get; }
        public ITokenReceiver<string> LocalTokenReceiver { get; }

        public WebApiAggregatorV1()
        {
            WebApiRequestFactoryV1 factory = new WebApiRequestFactoryV1();

            Registrator = factory.CreateRegistrator();
            Authenticator = factory.CreateAuthentificator();
            PasswordRestorer = factory.CreatePasswordRestorer();
            TokenReceiver = factory.CreateTokenReceiver();
            LocalTokenReceiver = factory.CreateLocalTokenReceiver();
        }
    }
}