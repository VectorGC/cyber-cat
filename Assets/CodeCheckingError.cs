using Newtonsoft.Json;

public class CodeCheckingError : ICodeConsoleMessage
{
    [JsonProperty("msg")] public string Msg { get; set; }
    [JsonProperty("expected")] public string Expected { get; set; }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(Msg))
        {
            return $"Не правильный вывод, ожидается '{Expected}'";
        }

        return Msg;
    }

    public string Message => ToString();
    public ConsoleMessageType MessageType => ConsoleMessageType.Error;
}