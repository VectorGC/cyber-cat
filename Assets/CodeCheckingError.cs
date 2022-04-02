using Newtonsoft.Json;

public class CodeCheckingError : ICodeConsoleMessage
{
    [JsonProperty("expected")] public string Expected { get; set; }

    public override string ToString()
    {
        return $"Не правильный вывод, ожидается '{Expected}'";
    }

    public string Message => ToString();
    public ConsoleMessageType MessageType => ConsoleMessageType.Error;
}