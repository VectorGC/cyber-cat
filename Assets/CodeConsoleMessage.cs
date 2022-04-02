using UniRx;

public readonly struct CodeConsoleMessage
{
    public string Message { get; }
    public ConsoleMessageType Type { get; }

    public CodeConsoleMessage(string message, ConsoleMessageType type = ConsoleMessageType.Log)
    {
        Message = message;
        Type = type;
    }

    public void Publish()
    {
        MessageBroker.Default.Publish(this);
    }

    public void Deconstruct(out string message, out ConsoleMessageType logType)
    {
        message = Message;
        logType = Type;
    }
}