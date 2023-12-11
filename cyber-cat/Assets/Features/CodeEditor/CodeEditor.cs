using System;
using ApiGateway.Client;
using ApiGateway.Client.Application;
using Cysharp.Threading.Tasks;
using Features.ServerConfig;
using Shared.Models.Domain.Tasks;
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

    public TaskDescription Task { get; private set; }

    public void Open(TaskDescription task) => OpenAsync(task).Forget();

    private async UniTaskVoid OpenAsync(TaskDescription task)
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
    public async UniTask LoadDebugTaskCheat(TaskType taskType)
    {
        var client = new ApiGatewayClient(ServerAPI.ServerEnvironment);
        var task = await client.TaskRepository.GetTaskDescription(taskType.Id());
        Task = task;
    }
#endif
}