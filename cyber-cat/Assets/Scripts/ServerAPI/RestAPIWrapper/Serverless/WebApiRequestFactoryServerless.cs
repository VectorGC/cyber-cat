using ServerAPIBase;

namespace RestAPIWrapper.Serverless
{
    internal class WebApiRequestFactoryServerless : WebApiRequestFactory<string>
    {
        public override IAuthenticator<string> CreateAuthentificator()
        {
            return new AuthenticatorServerless();
        }

        public override ICodeReceiver<string> CreateCodeReceiver()
        {
            return new CodeReceiverServerless();
        }

        public override ICodeSender<string> CreateCodeSender()
        {
            return new CodeSenderServerless();
        }

        public override IPasswordRestorer<string> CreatePasswordRestorer()
        {
            return new PasswordRestorerServerless();
        }

        public override IRegistrator<string> CreateRegistrator()
        {
            return new RegistratorServerless();
        }

        public override ITasksGetter<string> CreateTasksGetter()
        {
            return new TasksGetterServerless();
        }

        public override ITokenReceiver<string> CreateTokenReceiver()
        {
            return new TokenReceiverServerless();
        }
    }
}