using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ErrorMessageView : MonoBehaviour, IObserver<Exception>
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private GameObject exceptionObservableGo;

    private IObservable<Exception> _observableException;

    private void Start()
    {
        _observableException.Subscribe(this);
        text.text = string.Empty;
    }

    public void OnCompleted()
    {
        text.text = string.Empty;
    }

    public void OnError(Exception error) => text.text = error.Message;

    public void OnNext(Exception value) => OnError(value);

    private void OnValidate()
    {
        TryGetComponent(out text);
        var success = exceptionObservableGo.TryGetComponent(out _observableException);
        if (!success)
        {
            Debug.LogError(
                $"[ErrorMessageView] exceptionObservableGo not implement {_observableException.GetType()}");
            exceptionObservableGo = null;
        }
    }
}