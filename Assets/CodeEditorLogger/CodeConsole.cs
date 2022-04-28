using System;
using mikinel.easylogview;
using UniRx;

public class CodeConsole : EasyLogView
{
    public static void WriteLine(ICodeConsoleMessage codeConsoleMessage)
    {
        var message = codeConsoleMessage.Message;
        var type = codeConsoleMessage.MessageType;

        WriteLine(message, type);
    }

    public static void WriteLine(Exception exception) =>
        WriteLine(exception.Message, LogMessageType.Error);

    public static void WriteLine(string message, LogMessageType type = LogMessageType.Log) =>
        new CodeConsoleMessage(message, type).Publish();

    private void Awake()
    {
        MessageBroker.Default.Receive<ICodeConsoleMessage>()
            .Subscribe(OnLogMessage);
    }

    private readonly struct CodeConsoleMessage : ICodeConsoleMessage
    {
        public string Message { get; }
        public LogMessageType MessageType { get; }

        public CodeConsoleMessage(string message, LogMessageType type = LogMessageType.Log)
        {
            Message = message;
            MessageType = type;
        }

        public void Publish()
        {
            MessageBroker.Default.Publish<ICodeConsoleMessage>(this);
        }
    }

    private void OnLogMessage(ICodeConsoleMessage msg)
    {
        var message = msg.Message;
        var type = msg.MessageType;

        base.OnLogMessage(message, type);
    }

    // ReSharper disable once Unity.RedundantEventFunction
    private void OnDestroy()
    {
    }
}