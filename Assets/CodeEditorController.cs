using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
    
    public static async UniTask OpenEditorForTask(string taskId, ScheduledNotifier<float> progress = null)
    {
        progress ??= new ScheduledNotifier<float>();

        var requestProgress = new ScheduledNotifier<float>();
        var loadCodeEditorProgress = new ScheduledNotifier<float>();

        requestProgress.Union(loadCodeEditorProgress)
            .ReportTo(progress);

        var task = await new GetTaskRequest(taskId).SendRequest(requestProgress).ToUniTask();
        await OpenEditorForTask(task, loadCodeEditorProgress);
    }

    public static async UniTask OpenEditorForTask(ITaskTicket task, ScheduledNotifier<float> progress = null)
    {
        Debug.Log($"Opening code editor for task '{task.Id}'");

        await SceneManager.LoadSceneAsync("Code_editor_Blue", LoadSceneMode.Additive).ToUniTask(progress);
        MessageBroker.Default.Publish(new SetupTaskDescriptionMessage(task));
    }

    public static int GetOpenedTaskId() => Instance._openedTaskId;
    public static string GetCode() => Instance.codeInputField.text;

    private static CodeEditorController Instance => FindObjectOfType<CodeEditorController>();
}