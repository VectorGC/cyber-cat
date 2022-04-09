using System;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Extensions.UniRxExt;
using JetBrains.Annotations;
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
    private const string CodeEditorScene = "CodeEditor";

    [SerializeField] private TMP_InputField codeInputField;

    private ITaskTicket _task;

    private IDisposable _setTaskInEditorUnsubcriber;
    private IDisposable _progLanguageChangedUnsubscriber;

    public static ITaskTicket Task => Instance._task;

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

    public static async UniTask WaitWhenEnable()
    {
        var scene = SceneManager.GetSceneByName(CodeEditorScene);
        await UniTask.WaitWhile(() => scene.isLoaded);
    }

    private static void SetTaskInEditor(ITaskTicket task)
    {
        var message = new SetTaskInEditor(task);
        MessageBroker.Default.Publish(message);
    }

    protected override void OnEnable()
    {
        _setTaskInEditorUnsubcriber = MessageBroker.Default.Receive<SetTaskInEditor>().Subscribe(OnSetTaskInEditor);
        _progLanguageChangedUnsubscriber =
            MessageBroker.Default.Receive<ProgLanguageChanged>().Subscribe(OnProgLanguageChanged);
    }

    protected override void OnDisable()
    {
        _setTaskInEditorUnsubcriber.Dispose();
        _progLanguageChangedUnsubscriber.Dispose();
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