public struct CodeCheckingSuccess : ICodeConsoleMessage
{
    public override string ToString()
    {
        return "Задача решена";
    }

    public (string, ConsoleMessageType) GetConsoleMessage() => (ToString(), ConsoleMessageType.Success);
    public string Message => ToString();
    public ConsoleMessageType MessageType => ConsoleMessageType.Success;
}