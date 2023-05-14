namespace ServerAPIBase
{
    /// <summary>
    /// Holds all web requests
    /// </summary>
    /// <typeparam name="T">Requests return type</typeparam>
    public interface IWebApiAggregator<T>
    {
        IRegistrator<string> Registrator { get; }
        IAuthenticator<string> Authenticator { get; }
        IPasswordRestorer<string> PasswordRestorer { get; }
        ITokenReceiver<string> TokenReceiver { get; }
        ICodeSender<string> CodeSender { get; }
        ICodeReceiver<string> CodeReceiver { get; }
        ITasksGetter<string> TasksGetter { get; }
    }
}