using System;
using TMPro;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class LogMessageView : UIBehaviour, IObserver<LogEntry>
{
    [SerializeField] private TMP_Text text;

    protected override void Start()
    {
        text.text = string.Empty;
        ObservableLogger.Listener.Subscribe(this);
    }

    public void OnCompleted()
    {
        text.text = string.Empty;
    }

    public void SetGoodColor()
    {
        text.color = Color.green;
    }

    private void SetBadColor()
    {
        text.color = Color.red;
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(LogEntry value)
    {
        switch (value.LogType)
        {
            case LogType.Error:
            case LogType.Exception:
                SetBadColor();
                break;
            default:
                SetGoodColor();
                break;
        }

        text.text = value.Message;
    }
}