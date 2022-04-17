using JetBrains.Annotations;
using Newtonsoft.Json;
using RestAPIWrapper;

public class CodeCheckingResult : ICodeConsoleMessage
{
    [CanBeNull] [JsonProperty("error")] public int Error { get; set; }
    [CanBeNull] [JsonProperty("error_data")] public CodeCheckingError ErrorData { get; set; }
    public string Message 
    { 
        get 
        {return ErrorData==null? new CodeCheckingSuccess().Message :GetErrorMsg();} 
    }
    private string GetErrorMsg()
    {
        switch ((WebError)Error)
        {
            case WebError.SolutionTestFail:
                return ErrorData?.ToString();
            case WebError.SolutionBuildFail:
                return ErrorData?.Msg;
            case WebError.SolutionRuntimeFail:
                return WebErrorLocalize.Localize((WebError) Error) + $". {ErrorData?.Msg}";
            case WebError.SolutionTimeoutFail:
                return WebErrorLocalize.Localize((WebError) Error);
            default:
                return ErrorData?.Msg;
        }
    }
    public ConsoleMessageType MessageType => ErrorData?.MessageType ?? new CodeCheckingSuccess().MessageType;
}