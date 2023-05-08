using JetBrains.Annotations;
using Newtonsoft.Json;
using ServerAPIBase;

namespace RestAPIWrapper
{
    internal class VerdictResult : ICodeConsoleMessage
    {
        [JsonProperty("error")] public int Error { get; set; }

        [CanBeNull]
        [JsonProperty("error_data")]
        public VerdictError ErrorData { get; set; }

        public string Message => ErrorData == null ? new VerdictSuccess().Message : GetErrorMsg();

        private string GetErrorMsg()
        {
            switch ((WebError)Error)
            {
                case WebError.SolutionTestFail:
                    return ErrorData?.ToString();
                case WebError.SolutionBuildFail:
                    return ErrorData?.Msg;
                case WebError.SolutionRuntimeFail:
                    return WebErrorLocalize.Localize((WebError)Error) + $". {ErrorData?.Msg}";
                case WebError.SolutionTimeoutFail:
                    return WebErrorLocalize.Localize((WebError)Error);
                default:
                    return ErrorData?.Msg;
            }
        }

        public LogMessageType MessageType => ErrorData?.MessageType ?? new VerdictSuccess().MessageType;
    }
}