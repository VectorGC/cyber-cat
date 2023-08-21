public interface ICodeConsoleView
{
    void Log(string message);
    void LogError(string message);
    void LogSuccess(string message);
}