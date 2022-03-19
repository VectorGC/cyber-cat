using System;
using UniRx;
using UnityEngine.SceneManagement;

public class SceneActiveObservable : IObservable<Scene>, IDisposable
{
    private readonly string _sceneName;

    private readonly Subject<Scene> _sceneActiveObservable = new Subject<Scene>();

    public SceneActiveObservable(string sceneName)
    {
        _sceneName = sceneName;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!IsTargetScene(scene))
        {
            return;
        }

        _sceneActiveObservable.OnNext(scene);
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (!IsTargetScene(scene))
        {
            return;
        }

        _sceneActiveObservable.OnCompleted();
    }

    private bool IsTargetScene(Scene scene) => scene.name == _sceneName;

    public void Dispose()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;

        _sceneActiveObservable?.Dispose();
    }

    public IDisposable Subscribe(IObserver<Scene> observer) => _sceneActiveObservable.Subscribe(observer);
}