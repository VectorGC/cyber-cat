using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class CodeEditor : ICodeEditor
{
    public static bool IsOpen { get; private set; }
    public string TaskId { get; }

    public static IEnumerator Open(string taskId)
    {
        return OpenAsync(taskId).ToCoroutine();
    }

    private static async UniTask OpenAsync(string taskId)
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
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask(progress);
        IsOpen = false;
    }
}