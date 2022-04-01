public class CodeCheckingSuccess : ICodeConsoleMessage
{
    public override string ToString()
    {
        return "Задача решена";
    }

    public CodeConsoleMessage GetConsoleMessage()
    {
        return new CodeConsoleMessage(ToString());
    }
}