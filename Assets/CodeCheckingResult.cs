using JetBrains.Annotations;
using Newtonsoft.Json;

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
                return $"������ ��� �����: '{ErrorData.Params}'\n��� �����: {ErrorData.Result}\n���������: '{ErrorData.Expected}'";
            case WebError.SolutionBuildFail:
                return ErrorData.Msg;
            default:
                return "����������� ������. ���������� � ������";
        }
    }
    public ConsoleMessageType MessageType => ErrorData?.MessageType ?? new CodeCheckingSuccess().MessageType;
}