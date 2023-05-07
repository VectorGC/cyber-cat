namespace ServerAPIBase
{
    public interface IPasswordRestorer<T> : IWebApiRequester<IPasswordRestorerData, T>
    {

    }

    public interface IPasswordRestorerData
    {
        string Login { get; }
        string Password { get; }
    }

    public class PasswordRestorerData : IPasswordRestorerData
    {
        public string Login { get; }
        public string Password { get; }

        public PasswordRestorerData(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}