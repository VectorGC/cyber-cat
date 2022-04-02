using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class ErrorMessageView : UIBehaviour, IObserver<Exception>
{
    [SerializeField] private TMP_Text text;

    protected override void Awake()
    {
        MessageBroker.Default.Receive<Exception>().Subscribe(OnError);
    }

    protected override void Start()
    {
        text.text = string.Empty;
    }

    public void OnCompleted()
    {
        text.text = string.Empty;
    }

    public void OnError(Exception error) => text.text = error.Message;

    public void OnNext(Exception value) => OnError(value);
}