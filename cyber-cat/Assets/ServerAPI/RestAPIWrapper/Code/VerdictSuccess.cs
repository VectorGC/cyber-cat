namespace RestAPIWrapper
{
    internal struct VerdictSuccess : ICodeConsoleMessage
    {
        public override string ToString()
        {
            return "Ваш код сохранен. Приступайте к следующей задаче ^_^";
        }

        public string Message => ToString();
        public LogMessageType MessageType => LogMessageType.Success;
    }
}