using Newtonsoft.Json;

public class CodeCheckingError : ICodeConsoleMessage
{
    [JsonProperty("expected")]
    public string Expected { get; set; }
    
    public override string ToString()
    {
        return $"Не правильный вывод, ожидается '{Expected}'";
    }

    public CodeConsoleMessage GetConsoleMessage()
    {
        return new CodeConsoleMessage(ToString(), ConsoleMessageType.Error);
    }
}