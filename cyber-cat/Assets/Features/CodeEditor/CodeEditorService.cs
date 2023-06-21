using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class CodeEditorService : ICodeEditorService
{
    public async UniTaskVoid OpenEditor(string taskId, IProgress<float> progress = null)
    {
        if (string.IsNullOrEmpty(taskId))
        {
            throw new ArgumentNullException(nameof(taskId));
        }

        CodeEditorOpenedTask.TaskId = taskId;
        await SceneManager.LoadSceneAsync("CodeEditor", LoadSceneMode.Additive).ToUniTask(progress);
    }

    public async UniTaskVoid CloseEditor(IProgress<float> progress = null)
    {
        await SceneManager.UnloadSceneAsync("CodeEditor").ToUniTask(progress);
    }
}