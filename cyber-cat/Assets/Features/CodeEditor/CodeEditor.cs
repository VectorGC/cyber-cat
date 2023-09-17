using System;
using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class CodeEditor : ICodeEditor
{
    public event Action Closed;

    public bool IsOpen => SceneManager.GetSceneByName("CodeEditor").isLoaded;
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

        /*
        var editor = new CodeEditor(task);
        var controller = Object.FindObjectOfType<CodeEditorController>();
        controller.Construct(editor);
        */

        //return editor;
    }

    public void Close() => CloseAsync().Forget();

    private async UniTaskVoid CloseAsync()
    {
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask();
        Closed?.Invoke();
    }
}