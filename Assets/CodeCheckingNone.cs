public class CodeCheckingNone : ICodeConsoleMessage
{
    public override string ToString()
    {
        return "Нет данных по проверке";
    }

    public string Message => ToString();
    public ConsoleMessageType MessageType => ConsoleMessageType.Log;
}