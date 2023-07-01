using ServerAPIBase;

namespace RestAPIWrapper.V1
{
    internal class WebApiRequestFactoryV1 : WebApiRequestFactory<string>
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

        public override ICodeSender<string> CreateCodeSender()
        {
            return new CodeSenderV1();
        }

        public override ICodeReceiver<string> CreateCodeReceiver()
        {
            return new CodeReceiverV1();
        }

        public override ITasksGetter<string> CreateTasksGetter()
        {
            return new TasksGetterV1();
        }
    }
}