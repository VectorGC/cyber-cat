using mikinel.easylogview;
using UniRx;
using UnityEngine;

public readonly struct CodeConsoleMessage
{
    public string Message { get; }
    public LogType Type { get; }

    public CodeConsoleMessage(string message, LogType type = LogType.Log)
    {
        Message = message;
        Type = type;
    }

    public void Publish()
    {
        MessageBroker.Default.Publish(this);
    }

    public void Deconstruct(out string message, out LogType logType)
    {
        message = Message;
        logType = Type;
    }
}

public class CodeConsole : EasyLogView
{
    public static void WriteLine(string message, LogType type = LogType.Log) =>
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