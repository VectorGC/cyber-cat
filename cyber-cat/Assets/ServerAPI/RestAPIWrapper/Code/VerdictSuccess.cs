namespace RestAPIWrapper
{
    internal struct VerdictSuccess : ICodeConsoleMessage
    {
        public override string ToString()
        {
            return "��� ��� ��������. ����������� � ��������� ������ ^_^";
        }

        public string Message => ToString();
        public LogMessageType MessageType => LogMessageType.Success;
    }
}