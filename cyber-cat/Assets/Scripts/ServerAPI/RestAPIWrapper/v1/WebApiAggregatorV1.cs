using ServerAPIBase;

namespace RestAPIWrapper.V1
{
    public class WebApiAggregatorV1 : IWebApiAggregator<string>
    {
        public IRegistrator<string> Registrator { get; }
        public IAuthenticator<string> Authenticator { get; }
        public IPasswordRestorer<string> PasswordRestorer { get; }
        public ITokenReceiver<string> TokenReceiver { get; }
        public ICodeSender<string> CodeSender { get; }
        public ICodeReceiver<string> CodeReceiver { get; }
        public ITasksGetter<string> TasksGetter { get; }
        public ITokenReceiver<TokenSession> LocalTokenReceiver { get; }


        public WebApiAggregatorV1()
        {
            WebApiRequestFactoryV1 factory = new WebApiRequestFactoryV1();

            Registrator = factory.CreateRegistrator();
            Authenticator = factory.CreateAuthentificator();
            PasswordRestorer = factory.CreatePasswordRestorer();
            TokenReceiver = factory.CreateTokenReceiver();
            LocalTokenReceiver = factory.CreateLocalTokenReceiver();
            CodeSender = factory.CreateCodeSender();
            TasksGetter = factory.CreateTasksGetter();
        }
    }
}