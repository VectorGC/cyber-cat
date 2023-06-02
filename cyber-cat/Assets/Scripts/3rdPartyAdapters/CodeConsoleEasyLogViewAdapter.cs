using mikinel.easylogview;

public class CodeConsoleEasyLogViewAdapter : EasyLogView, ICodeConsoleView
{
    public void Log(string message) => OnLogMessage(message, LogMessageType.Log);

    public void LogError(string message) => OnLogMessage(message, LogMessageType.Error);

    public void LogSuccess(string message) => OnLogMessage(message, LogMessageType.Success);
}