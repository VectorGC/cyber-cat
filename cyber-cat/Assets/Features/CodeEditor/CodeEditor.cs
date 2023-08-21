using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class CodeEditor : ICodeEditor
{
    public static bool IsOpen { get; private set; }
    public string TaskId { get; }

    public static void Open(string taskId)
    {
        OpenAsync(taskId).Forget();
    }

    private static async UniTaskVoid OpenAsync(string taskId)
    {
        if (string.IsNullOrEmpty(taskId))
        {
            throw new ArgumentNullException(nameof(taskId));
        }

        await SceneManager.LoadSceneAsync("CodeEditor", LoadSceneMode.Additive).ToUniTask();

        var editor = new CodeEditor(taskId);
        var controller = Object.FindObjectOfType<CodeEditorController>();
        await controller.Construct(editor);
    }

    private CodeEditor(string taskId)
    {
        TaskId = taskId;
        IsOpen = true;
    }

    public void Close()
    {
        CloseAsync().Forget();
    }

    private async UniTaskVoid CloseAsync(IProgress<float> progress = null)
    {
        IsOpen = false;
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask(progress);
    }
}