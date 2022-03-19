using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodeEditorObservable : IObservable<AsyncOperation>
{
    private readonly IAsyncOperationObservable _asyncOperationObservable;
    private readonly IObservable<Scene> _sceneActiveObservable;

    public CodeEditorObservable(IObservable<AsyncOperation> asyncOperationObservable,
        IObservable<float> progressObservable)
    {
        _asyncOperationObservable = new AsyncOperationObservable(asyncOperationObservable, progressObservable);
        _sceneActiveObservable = new SceneActiveObservable("Code_editor_Blue");
    }

    public IDisposable WhileOpen(IObserver<Scene> observer) => _sceneActiveObservable.Subscribe(observer);
    public IObservable<float> ProgressObservable() => _asyncOperationObservable;
    
    public IDisposable Subscribe() => _asyncOperationObservable.Subscribe();
    public IDisposable Subscribe(IObserver<AsyncOperation> observer) => _asyncOperationObservable.Subscribe(observer);
}