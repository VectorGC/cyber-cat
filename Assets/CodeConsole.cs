using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MessageType
{
    Default = 0,
    Error,
    Alert
}

public struct CodeConsoleMessage
{
    public string Message { get; }
    public MessageType Type { get; }

    public CodeConsoleMessage(string message, MessageType type = MessageType.Default)
    {
        Message = message;
        Type = type;
    }
}

public class CodeConsole : UIBehaviour
{
    [SerializeField]
    private Text consoleText;

    protected override void Awake()
    {
        MessageBroker.Default.Receive<ICodeTestingResult>().Subscribe(OnReceiveCodeTestingResult);
        MessageBroker.Default.Receive<CodeConsoleMessage>().Subscribe(WriteLine);
    }

    private void OnReceiveCodeTestingResult(ICodeTestingResult codeTestingResult)
    {
        switch (codeTestingResult)
        {
            case ErrorCodeTestingResult error:
                WriteLine(error.Message, MessageType.Error);
                break;
            default:
                WriteLine(codeTestingResult.Message);
                break;
        }
    }

    private void WriteLine(CodeConsoleMessage codeConsoleMessage) =>
        WriteLine(codeConsoleMessage.Message, codeConsoleMessage.Type);

    private void WriteLine(string msg, MessageType type = MessageType.Default)
    {
        switch (type)
        {
            case MessageType.Error:
                WriteLine(msg, Color.red);
                return;
            case MessageType.Alert:
                WriteLine(msg, Color.yellow);
                return;
            default:
                WriteLine(msg, Color.white);
                return;
        }
    }

    private void WriteLine(string msg, Color color)
    {
        consoleText.color = color;
        consoleText.text = msg;
    }

    public void Clear()
    {
        consoleText.text = string.Empty;
    }
}