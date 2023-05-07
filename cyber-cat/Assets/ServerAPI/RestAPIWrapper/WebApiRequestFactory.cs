using ServerAPIBase;

namespace RestAPIWrapper
{
    public abstract class WebApiRequestFactory<T>
    {
        public abstract IRegistrator<T> CreateRegistrator();
        public abstract IAuthenticator<T> CreateAuthentificator();
        public abstract IPasswordRestorer<T> CreatePasswordRestorer();
        public abstract ITokenReceiver<T> CreateTokenReceiver();
    }
}