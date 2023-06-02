namespace ServerAPIBase
{
    public interface IRegistrator<T> : IWebApiRequester<IRegistratorData, T>
    {

    }

    public interface IRegistratorData
    {
        string Login { get; }
        string Password { get; }
    }

    public class RegistratorData : IRegistratorData
    {
        public string Login { get; }
        public string Password { get; }

        public RegistratorData(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}