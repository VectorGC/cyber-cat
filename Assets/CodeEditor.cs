using System;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Extensions.UniRxExt;
using RestAPIWrapper;
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

    public static ITaskTicket Task
    {
        get => Instance._task;
        set => Instance._task = value;
    }

    public static string Code => Instance.codeInputField.text;
    public static ProgLanguage Language { get; private set; }

    private static CodeEditor Instance => FindObjectOfType<CodeEditor>();

    public static async UniTask OpenSolution(ITaskTicket task, IProgress<float> progress = null)
    {
        await SceneManager.LoadSceneAsync(CodeEditorScene, LoadSceneMode.Additive).ToUniTask(progress);
        SetTaskInEditor(task);
    }

    public static async UniTask OpenSolution(TaskFolder taskFolder, IProgress<float> progress = null)
    {
        var requestProgress = new ScheduledNotifier<float>();
        var openEditorProgress = new ScheduledNotifier<float>();

        requestProgress.Union(openEditorProgress).ReportTo(progress);

        var token = TokenSession.FromPlayerPrefs();
        var task = await taskFolder.GetTask(token, requestProgress);

        await OpenSolution(task, openEditorProgress);
    }

    private static void SetTaskInEditor(ITaskTicket task)
    {
        var message = new SetTaskInEditor(task);
        MessageBroker.Default.Publish(message);
    }

    protected override void Awake()
    {
        MessageBroker.Default.Receive<SetTaskInEditor>().Subscribe(OnSetTaskInEditor);
        MessageBroker.Default.Receive<ProgLanguageChanged>().Subscribe(OnProgLanguageChanged);
    }

    private void OnSetTaskInEditor(SetTaskInEditor message)
    {
        _task = message.TaskTicket;
        SetDescription(_task);
    }

    private void SetDescription(ITaskTicket task)
    {
        var message = new SetTaskDescriptionMessage(task);
        MessageBroker.Default.Publish(message);
    }

    private void OnProgLanguageChanged(ProgLanguageChanged msg)
    {
        Language = msg.Language;
    }
}