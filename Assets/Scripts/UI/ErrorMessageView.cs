using System;
using Authentication;
using TMPro;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ErrorMessageView : UIBehaviour, IObserver<LogEntry>
{
    [SerializeField] private TMP_Text text;

    protected override void Start()
    {
        text.text = string.Empty;
        ObservableLogger.Listener.Subscribe(this);

        var lp = new UniRx.Diagnostics.Logger(nameof(TokenWebRequestWrapper));
        lp.ThrowException(new RequestTokenException("123213213"));
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
        SetBadColor();
        text.text = error.Message;
    }

    public void OnNext(LogEntry value)
    {
    }
}