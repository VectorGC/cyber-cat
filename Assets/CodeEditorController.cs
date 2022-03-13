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

    public static IObservable<float> OpenEditorForTask(string taskId, ScheduledNotifier<float> progress = null)
    {
        var requestProgress = new ScheduledNotifier<float>();
        var loadCodeEditorProgress = new ScheduledNotifier<float>();

        new GetTaskRequest(taskId)
            .SendRequest()
            .ContinueWith(x => OpenEditorForTaskObservable(x, loadCodeEditorProgress))
            .Subscribe();

        return requestProgress.Union(loadCodeEditorProgress).ReportTo(progress);
    }

    public static IObservable<AsyncOperation> OpenEditorForTaskObservable(ITaskTicket task,
        ScheduledNotifier<float> progress = null)
    {
        Debug.Log($"Opening code editor for task '{task.Id}'");

        return SceneManager.LoadSceneAsync("Code_editor_Blue")
            .ViaLoadingScreenObservable(progress)
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