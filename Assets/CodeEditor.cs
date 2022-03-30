using System;
using Cysharp.Threading.Tasks;
using Extensions.UniRxExt;
using TasksData.Requests;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public readonly struct SetTaskInEditor
{
    public ITaskTicket TaskTicket { get; }

    public SetTaskInEditor(ITaskTicket taskTicket)
    {
        TaskTicket = taskTicket;
    }
}

public class CodeEditor : UIBehaviour
{
    private const string CodeEditorScene = "Code_editor_Blue";

    [SerializeField] private TMP_InputField codeInputField;

    private ITaskTicket _task;
    public static ITaskTicket Task => Instance._task;
    
    public static async UniTask OpenEditorForTask(string taskId, ScheduledNotifier<float> progress = null)
    {
        progress ??= new ScheduledNotifier<float>();

        var requestProgress = new ScheduledNotifier<float>();
        var loadCodeEditorProgress = new ScheduledNotifier<float>();

        requestProgress.Union(loadCodeEditorProgress).ReportTo(progress);

        var task = await new GetTaskRequest(taskId).SendRequest(requestProgress).ToUniTask();
        await OpenEditorForTask(task, loadCodeEditorProgress);
    }

    public static async UniTask OpenEditorForTask(ITaskTicket task, ScheduledNotifier<float> progress = null)
    {
        Debug.Log($"Opening code editor for task '{task.Id}'");

        await SceneManager.LoadSceneAsync("Code_editor_Blue", LoadSceneMode.Additive).ToUniTask(progress);
        MessageBroker.Default.Publish(new SetTaskDescriptionMessage(task));
    }

    public static string Code => Instance.codeInputField.text;

    private static CodeEditor Instance => FindObjectOfType<CodeEditor>();

    public static async UniTask OpenSolution(ITaskTicket task, IProgress<float> progress = null)
    {
        await SceneManager.LoadSceneAsync(CodeEditorScene, LoadSceneMode.Additive).ToUniTask(progress);
        SetTaskInEditor(task);
    }

    private static void SetTaskInEditor(ITaskTicket task)
    {
        var message = new SetTaskInEditor(task);
        MessageBroker.Default.Publish(message);
    }

    protected override void Awake()
    {
        MessageBroker.Default.Receive<SetTaskInEditor>().Subscribe(SetTaskInEditor);
    }

    private void SetTaskInEditor(SetTaskInEditor message)
    {
        _task = message.TaskTicket;
        SetDescription(_task);
    }

    private void SetDescription(ITaskTicket task)
    {
        var message = new SetTaskDescriptionMessage(task);
        MessageBroker.Default.Publish(message);
    }
}