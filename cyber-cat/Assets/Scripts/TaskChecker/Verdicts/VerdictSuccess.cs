namespace TaskChecker.Verdicts
{
    internal struct VerdictSuccess : ICodeConsoleMessage
    {
        public override string ToString()
        {
            return "Задача решена";
        }
    
        public string Message => ToString();
        public LogMessageType MessageType => LogMessageType.Success;
    }
}