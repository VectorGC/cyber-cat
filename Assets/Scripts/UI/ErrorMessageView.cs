using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ErrorMessageView : MonoBehaviour, IObserver<Exception>
{
    [SerializeField] private TMP_Text text;

    [FormerlySerializedAs("exceptionObservableGo")] [SerializeField]
    private List<GameObject> exceptionObservablesGo;

    private readonly List<IObservable<Exception>> _observablesExceptionGo = new List<IObservable<Exception>>();

    private void Start()
    {
        AddObservablesGoToObservables();
        SubscribeToObservablesMessages();

        text.text = string.Empty;
    }

    private void AddObservablesGoToObservables()
    {
        foreach (var observableGo in exceptionObservablesGo)
        {
            IObservable<Exception> observableException = null;
            var success = observableGo && observableGo.TryGetComponent(out observableException);
            if (success)
            {
                _observablesExceptionGo.Add(observableException);
            }
        }
    }

    private void SubscribeToObservablesMessages()
    {
        foreach (var observable in _observablesExceptionGo)
        {
            observable.Subscribe(this);
        }
    }

    public void OnCompleted()
    {
        text.text = string.Empty;
    }

    public void OnError(Exception error) => text.text = error.ToString();

    public void OnNext(Exception value) => OnError(value);

    private void OnValidate()
    {
        var existsComponent = TryGetComponent(out text);
        if (!existsComponent)
        {
            Debug.LogError("[ErrorMessageView] Required text component");
        }
    }
}