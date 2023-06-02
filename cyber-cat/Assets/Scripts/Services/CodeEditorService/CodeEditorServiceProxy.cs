using System;
using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine.SceneManagement;

public class CodeEditorServiceProxy : ICodeEditorService
{
    public ITask CurrentTask { get; private set; }

    private readonly IClient _client;

    public CodeEditorServiceProxy(IClient client)
    {
        _client = client;
    }

    public async UniTaskVoid OpenEditor(ITask task, IProgress<float> progress = null)
    {
        CurrentTask = task;
        await SceneManager.LoadSceneAsync("CodeEditor", LoadSceneMode.Additive).ToUniTask(progress);
    }

    public async UniTaskVoid CloseEditor(IProgress<float> progress = null)
    {
        CurrentTask = null;
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask(progress);
    }

    public UniTask<string> LoadSavedCode(ITask task, IProgress<float> progress = null)
    {
        throw new NotImplementedException();
    }

    public UniTask<IVerdict> VerifySolution(ITask task, string sourceCode, IProgress<float> progress = null)
    {
        throw new NotImplementedException();
    }
}