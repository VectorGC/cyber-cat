using System;
using UniRx;
using UnityEngine;

public class AsyncOperationObservable : IAsyncOperationObservable
{
    private readonly IObservable<AsyncOperation> _asyncOperationObservable;
    private readonly IObservable<float> _progressObservable;

    public AsyncOperationObservable(IObservable<AsyncOperation> asyncOperationObservable,
        IObservable<float> progressObservable)
    {
        _asyncOperationObservable = asyncOperationObservable;
        _progressObservable = progressObservable;
    }

    public IDisposable Subscribe() => _asyncOperationObservable.Subscribe();
    public IDisposable Subscribe(IObserver<AsyncOperation> observer) => _asyncOperationObservable.Subscribe(observer);
    public IDisposable Subscribe(IObserver<float> observer) => _progressObservable.Subscribe(observer);
}