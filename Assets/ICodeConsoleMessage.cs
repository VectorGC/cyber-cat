public interface ICodeConsoleMessage
{
    string Message { get; }
    ConsoleMessageType MessageType { get; }
}