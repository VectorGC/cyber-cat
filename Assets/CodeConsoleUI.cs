using System;
using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MessageType
{
    Default = 0,
    Error,
    Alert
}

public struct CodeConsoleWriteLineMessage
{
    public string Message { get; }
    public MessageType Type { get; }

    public CodeConsoleWriteLineMessage(string message, MessageType type = MessageType.Default)
    {
        Message = message;
        Type = type;
    }
}

public struct CodeConsoleWriteLineDelayedMessage
{
    public string Message { get; }
    public MessageType Type { get; }
    public TimeSpan Delay { get; }

    public CodeConsoleWriteLineDelayedMessage(string message, TimeSpan delay, MessageType type = MessageType.Default)
    {
        Message = message;
        Type = type;
        Delay = delay;
    }
}

public static class CodeConsole
{
    public static void WriteLine(string message, MessageType type = MessageType.Default)
    {
        var msg = new CodeConsoleWriteLineMessage(message, type);
        MessageBroker.Default.Publish(msg);
    }

    public static void WriteDelayedLine(string message, MessageType type = MessageType.Default)
    {
        var msg = new CodeConsoleWriteLineDelayedMessage(message, TimeSpan.FromSeconds(0.1f), type);
        AsyncMessageBroker.Default.PublishAsync(msg).StartAsCoroutine();
    }

    public static void WriteDelayedLine(string message, float delayInSeconds, MessageType type = MessageType.Default)
    {
        WriteDelayedLine(message, TimeSpan.FromSeconds(delayInSeconds), type);
    }

    public static void WriteDelayedLine(string message, TimeSpan delay, MessageType type = MessageType.Default)
    {
        var msg = new CodeConsoleWriteLineDelayedMessage(message, delay, type);
        AsyncMessageBroker.Default.PublishAsync(msg).StartAsCoroutine();
    }
}

public class CodeConsoleUI : UIBehaviour
{
    [SerializeField] private TMP_Text consoleText;

    [Header("Colors")] [SerializeField] private Color alert = Color.yellow;
    [SerializeField] private Color error = Color.red;

    private Color _defaultColor;

    private readonly StringBuilder _stringBuilder = new StringBuilder();

    protected override void Awake()
    {
        _defaultColor = consoleText.color;

        MessageBroker.Default.Receive<CodeConsoleWriteLineMessage>().Subscribe(WriteLine);
        AsyncMessageBroker.Default.Subscribe<CodeConsoleWriteLineDelayedMessage>(WriteDelayedLine);
    }

    private IObservable<Unit> WriteDelayedLine(CodeConsoleWriteLineDelayedMessage arg)
    {
        var task = UniTask.Create(async () =>
        {
            var message = arg.Message;
            foreach (var symbol in message)
            {
                Write(symbol.ToString(), arg.Type);
                await UniTask.Delay(arg.Delay);
            }
        });

        return Observable.FromCoroutine(() => task.ToCoroutine());
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Test"))
        {
            CodeConsole.WriteLine("Test1", MessageType.Error);
            CodeConsole.WriteLine("Test2", MessageType.Alert);
            CodeConsole.WriteLine("Test3");

            CodeConsole.WriteDelayedLine("Test4");
        }
    }

    private void WriteLine(CodeConsoleWriteLineMessage codeConsoleMessage) =>
        WriteLine(codeConsoleMessage.Message, codeConsoleMessage.Type);

    private void WriteLine(string msg, MessageType type = MessageType.Default)
    {
        var color = GetColorByMessageType(type);
        WriteLine(msg, color);
    }

    private Color GetColorByMessageType(MessageType type)
    {
        switch (type)
        {
            case MessageType.Error:
                return error;
            case MessageType.Alert:
                return alert;
            default:
                return _defaultColor;
        }
    }

    private void WriteLine(string msg, Color color)
    {
        var hexColor = ColorUtility.ToHtmlStringRGB(color);
        _stringBuilder.AppendLine($"<color=#{hexColor}>{msg}</color>");
        consoleText.text = _stringBuilder.ToString();
    }

    private void Write(string msg, MessageType type = MessageType.Default)
    {
        var color = GetColorByMessageType(type);
        Write(msg, color);
    }

    private void Write(string msg, Color color)
    {
        var hexColor = ColorUtility.ToHtmlStringRGB(color);
        _stringBuilder.Append($"<color=#{hexColor}>{msg}</color>");
        consoleText.text = _stringBuilder.ToString();
    }
}