namespace ServerAPIBase
{
    /// <summary>
    /// Holds all web requests
    /// </summary>
    /// <typeparam name="T">Requests return type</typeparam>
    public interface IWebApiAggregator<T>
    {
        IRegistrator<T> Registrator { get; }
        IAuthenticator<T> Authenticator { get; }
        IPasswordRestorer<T> PasswordRestorer { get; }
    }
}