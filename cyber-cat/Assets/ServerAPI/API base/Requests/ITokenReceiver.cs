namespace ServerAPIBase
{
    public interface ITokenReceiver<T> : IWebApiRequester<ITokenReceiverData, T>
    {

    }

    public interface ITokenReceiverData
    {
        string Login { get; }
        string Password { get; }
    }

    public class TokenReceiverData : ITokenReceiverData
    {
        public string Login { get; }
        public string Password { get; }

        public TokenReceiverData(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}