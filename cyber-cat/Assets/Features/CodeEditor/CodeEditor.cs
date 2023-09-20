using System;
using System.Diagnostics;
using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using Features.ServerConfig;
using UnityEngine.SceneManagement;

public class CodeEditor : ICodeEditor
{
    public event Action Closed;

    public bool IsOpen
    {
        get
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == "CodeEditor")
                {
                    return true;
                }
            }

            return false;
        }
    }

    public ITask Task { get; private set; }

    public void Open(ITask task) => OpenAsync(task).Forget();

    private async UniTaskVoid OpenAsync(ITask task)
    {
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        Task = task;
        await SceneManager.LoadSceneAsync("CodeEditor", LoadSceneMode.Additive).ToUniTask();
    }

    public void Close() => CloseAsync().Forget();

    private async UniTaskVoid CloseAsync()
    {
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask();
        Closed?.Invoke();
    }

#if UNITY_EDITOR
    public async UniTask LoadTutorialCheat()
    {
        var player = await ServerAPI.CreatePlayerClient();
        var task = player.Tasks["tutorial"];
        Task = task;
    }
#endif
}