using Newtonsoft.Json;

public class CodeCheckingError : ICodeConsoleMessage
{
    [JsonProperty("stage")] public string Stage { get; set; }
    [JsonProperty("msg")] public string Msg { get; set; }
    [JsonProperty("expected")] public string Expected { get; set; }
    [JsonProperty("params")] public string Params { get; set; }
    [JsonProperty("result")] public string Result { get; set; }

    public override string ToString()
    {
        
        switch(Stage)
        {
            case "test":
                return $"Данные для теста: '{Params}'\nВаш вывод: {Result}\nОжидается: '{Expected}'";
            case "build":
                return Msg;
            default: 
                return "Неизвестная ошибка. Обратитесь к админу";
        }
    }

    public string Message => ToString();
    public ConsoleMessageType MessageType => ConsoleMessageType.Error;
}