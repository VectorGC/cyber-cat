public interface ICodeConsoleMessage
{
    string Message { get; }
    LogMessageType MessageType { get; }
}