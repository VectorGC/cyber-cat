using ServerAPIBase;

namespace RestAPIWrapper.Serverless
{
    public class CodeConsoleRestAPIMessage : ICodeConsoleMessage
    {
        public CodeConsoleRestAPIMessage(string message, LogMessageType messageType)
        {
            Message = message;
            MessageType = messageType;
        }

        public string Message { get; }
        public LogMessageType MessageType { get; }
    }
}