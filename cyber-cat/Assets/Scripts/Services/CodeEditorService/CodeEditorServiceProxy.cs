using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Models;
using Repositories.TaskRepositories;
using ServerAPI;
using UnityEngine.SceneManagement;

public class CodeEditorServiceProxy : ICodeEditorService
{
    [CanBeNull] private string _taskId;

    private readonly IServerAPI _serverAPI;
    private readonly ITaskRepository _taskRepository;

    public CodeEditorServiceProxy(IServerAPI serverAPI, ITaskRepository taskRepository)
    {
        _serverAPI = serverAPI;
        _taskRepository = taskRepository;
    }

    public async UniTaskVoid OpenEditor(string taskId, IProgress<float> progress = null)
    {
        if (string.IsNullOrEmpty(taskId))
        {
            throw new ArgumentNullException(nameof(taskId));
        }
        
        _taskId = taskId;
        await SceneManager.LoadSceneAsync("CodeEditor", LoadSceneMode.Additive).ToUniTask(progress);
    }

    public async UniTaskVoid CloseEditor(IProgress<float> progress = null)
    {
        _taskId = string.Empty;
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask(progress);
    }

    public async UniTask<ITask> GetCurrentTask()
    {
        if (string.IsNullOrEmpty(_taskId))
        {
            throw new ArgumentNullException(nameof(_taskId));
        }

        return await _taskRepository.GetTask(_taskId);
    }

    public async UniTask<string> GetSavedCode()
    {
        if (string.IsNullOrEmpty(_taskId))
        {
            throw new ArgumentNullException(nameof(_taskId));
        }

        return await _serverAPI.GetSavedCode(_taskId);
    }

    public async UniTask<IVerdict> VerifySolution(string sourceCode)
    {
        if (string.IsNullOrEmpty(_taskId))
        {
            throw new ArgumentNullException(nameof(_taskId));
        }

        return await _serverAPI.VerifySolution(_taskId, sourceCode);
    }
}