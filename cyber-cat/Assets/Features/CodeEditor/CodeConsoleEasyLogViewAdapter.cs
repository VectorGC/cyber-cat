using mikinel.easylogview;
using UnityEngine;

public class CodeConsoleEasyLogViewAdapter : CodeConsoleView
{
    [SerializeField] private EasyLogView _easyLogView;

    public override void Log(string message) => _easyLogView.LogMessage(message, LogMessageType.Log, false);

    public override void LogError(string message) => _easyLogView.LogMessage(message, LogMessageType.Error, false);

    public override void LogSuccess(string message) => _easyLogView.LogMessage(message, LogMessageType.Success, false);
}