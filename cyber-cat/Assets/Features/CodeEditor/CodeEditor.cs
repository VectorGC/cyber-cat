using System;
using System.Threading.Tasks;
using ApiGateway.Client.Models;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class CodeEditor : ICodeEditor
{
    public event Action Closed;

    public bool IsOpen { get; private set; }
    public ITask Task { get; }

    public static async Task<ICodeEditor> OpenAsync(ITask task)
    {
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        await SceneManager.LoadSceneAsync("CodeEditor", LoadSceneMode.Additive).ToUniTask();

        var editor = new CodeEditor(task);
        var controller = Object.FindObjectOfType<CodeEditorController>();
        controller.Construct(editor);

        editor.IsOpen = true;

        return editor;
    }

#if UNITY_EDITOR
    public static async Task<CodeEditor> CreateInstance(ITask task)
    {
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        var editor = new CodeEditor(task);
        return editor;
    }

#endif

    private CodeEditor(ITask task)
    {
        Task = task;
    }

    public void Close()
    {
        CloseAsync().Forget();
    }

    private async UniTaskVoid CloseAsync(IProgress<float> progress = null)
    {
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask(progress);
        IsOpen = false;
        Closed?.Invoke();
    }
}