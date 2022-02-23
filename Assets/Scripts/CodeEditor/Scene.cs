using System;
using UniRx;
using UnityEngine.SceneManagement;

public static class Scene
{
    public static void OpenScene(string sceneName, Action onCompleted)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            onCompleted?.Invoke();
            return;
        }

        SceneManager.LoadSceneAsync(sceneName)
            .AsAsyncOperationObservable()
            .Subscribe(x => onCompleted?.Invoke());
    }
}