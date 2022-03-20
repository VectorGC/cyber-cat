using System;
using Extensions.UniRxExt;
using TasksData.Requests;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodeEditorController : MonoBehaviour
{
    [SerializeField] private TMP_InputField codeInputField;

    private int _openedTaskId;

    public static CodeEditorObservable Open(string taskId)
    {
        var result = OpenEditorForTaskObserver(taskId);
        result.Subscribe<AsyncOperation>();

        return result;
    }

    private static CodeEditorObservable OpenEditorForTaskObserver(string taskId,
        ScheduledNotifier<float> progress = null)
    {
        progress ??= new ScheduledNotifier<float>();

        var requestProgress = new ScheduledNotifier<float>();
        var loadCodeEditorProgress = new ScheduledNotifier<float>();

        var progressObservable = requestProgress
            .Union(loadCodeEditorProgress)
            .ReportTo(progress);

        var operationObservable = new GetTaskRequest(taskId)
            .SendRequest(requestProgress)
            .ContinueWith(x => OpenEditorForTaskObservable(x, loadCodeEditorProgress));

        var t = new CodeEditorObservable(operationObservable, progressObservable);
        return t;
    }

    public static IObservable<AsyncOperation> OpenEditorForTaskObservable(ITaskTicket task,
        ScheduledNotifier<float> progress = null)
    {
        Debug.Log($"Opening code editor for task '{task.Id}'");

        return SceneManager.LoadSceneAsync("Code_editor_Blue", LoadSceneMode.Additive)
            .AsAsyncOperationObservable(progress)
            .DoOnCompleted(() =>
            {
                var codeEditorStartup = FindObjectOfType<CodeEditorStartup>();
                codeEditorStartup.SetupCodeEditorForTask(task);
            });
    }

    public static int GetOpenedTaskId() => Instance._openedTaskId;
    public static string GetCode() => Instance.codeInputField.text;

    public void SetupCodeEditor(ITaskTicket task)
    {
        _openedTaskId = task.Id;
    }

    private static CodeEditorController Instance => FindObjectOfType<CodeEditorController>();
}