public class CodeCheckingNone : ICodeConsoleMessage
{
    public CodeConsoleMessage GetConsoleMessage()
    {
        return new CodeConsoleMessage("Нет данных по проверке");
    }
}