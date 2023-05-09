using ServerAPIBase;

namespace RestAPIWrapper
{
    internal abstract class WebApiRequestFactory<T>
    {
        public abstract IRegistrator<T> CreateRegistrator();
        public abstract IAuthenticator<T> CreateAuthentificator();
        public abstract IPasswordRestorer<T> CreatePasswordRestorer();
        public abstract ITokenReceiver<T> CreateTokenReceiver();
        public abstract ICodeSender<T> CreateCodeSender();
        public abstract ICodeReceiver<T> CreateCodeReceiver();
        public abstract ITasksGetter<T> CreateTasksGetter();
    }
}