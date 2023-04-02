using System.Text.Json.Serialization;

namespace ApiGateway.Dto;

public class VerdictError
{
    [JsonPropertyName("stage")] public string Stage { get; set; } = null!;
    [JsonPropertyName("msg")] public string Msg { get; set; } = null!;
    [JsonPropertyName("expected")] public string Expected { get; set; } = null!;
    [JsonPropertyName("params")] public string Params { get; set; } = null!;
    [JsonPropertyName("result")] public string Result { get; set; } = null!;
    [JsonPropertyName("tests_passed")] public string TestsPassed { get; set; } = null!;
    [JsonPropertyName("tests_total")] public string TestsTotal { get; set; } = null!;

    public override string ToString()
    {
        return Stage switch
        {
            "build" => Msg,
            _ => $"Данные для теста: '{Params}'\nВаш вывод: {Result}\nОжидается: '{Expected}'. Тестов пройдено {TestsPassed} / {TestsTotal}"
        };
    }
}