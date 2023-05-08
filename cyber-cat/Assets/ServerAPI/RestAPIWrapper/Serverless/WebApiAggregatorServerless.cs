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

        }
    }
}