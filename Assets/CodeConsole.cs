using mikinel.easylogview;
using UniRx;

public class CodeConsole : EasyLogView
{
    public static void WriteLine(ICodeConsoleMessage codeConsoleMessage)
    {
        var msg = codeConsoleMessage.GetConsoleMessage();
        var (message, logType) = msg;
        WriteLine(message, logType);
    }

    public static void WriteLine(string message, ConsoleMessageType type = ConsoleMessageType.Log) =>
        new CodeConsoleMessage(message, type).Publish();

    private void Awake()
    {
        MessageBroker.Default.Receive<CodeConsoleMessage>()
            .Subscribe(OnLogMessage);
    }

    private void OnLogMessage(CodeConsoleMessage msg)
    {
        var (message, logType) = msg;
        
        base.OnLogMessage(message, string.Empty, logType);
    }

    // ReSharper disable once Unity.RedundantEventFunction
    private void OnDestroy()
    {
    }
}