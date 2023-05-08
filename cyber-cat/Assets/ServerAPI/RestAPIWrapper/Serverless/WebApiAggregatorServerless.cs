using ServerAPIBase;

namespace RestAPIWrapper.Serverless
{
    public class WebApiAggregatorServerless : IWebApiAggregator<string>
    {
        public IRegistrator<string> Registrator { get; }
        public IAuthenticator<string> Authenticator { get; }
        public IPasswordRestorer<string> PasswordRestorer { get; }
        public ITokenReceiver<string> TokenReceiver { get; }
        public ICodeSender<string> CodeSender { get; }
        public ICodeReceiver<string> CodeReceiver { get; }
        public ITasksGetter<string> TasksGetter { get; }

        public WebApiAggregatorServerless()
        {
            WebApiRequestFactoryServerless factory = new WebApiRequestFactoryServerless();

            Registrator = factory.CreateRegistrator();
            Authenticator = factory.CreateAuthentificator();
            PasswordRestorer = factory.CreatePasswordRestorer();
            TokenReceiver = factory.CreateTokenReceiver();
            CodeSender = factory.CreateCodeSender();
            CodeReceiver = factory.CreateCodeReceiver();
            TasksGetter = factory.CreateTasksGetter();
        }
    }
}