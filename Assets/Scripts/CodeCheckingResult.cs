using JetBrains.Annotations;
using Newtonsoft.Json;

public class CodeCheckingResult : ICodeConsoleMessage
{
    [CanBeNull] [JsonProperty("error")] public CodeCheckingError Error { get; set; }

    public string Message => Error?.Message ?? new CodeCheckingSuccess().Message;
    public ConsoleMessageType MessageType => Error?.MessageType ?? new CodeCheckingSuccess().MessageType;
}