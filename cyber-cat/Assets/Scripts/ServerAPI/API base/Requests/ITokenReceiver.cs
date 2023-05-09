namespace ServerAPIBase
{
    public interface ITokenReceiver<T> : IWebApiRequester<ITokenReceiverData, T>
    {

    }

    public interface ITokenReceiverData
    {
        string Login { get; }
        string Password { get; }
        string Email { get; }
    }

    public class TokenReceiverData : ITokenReceiverData
    {
        public string Login { get; }
        public string Password { get; }
        public string Email { get; }

        public TokenReceiverData(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}