using System;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Extensions.UniRxExt;
using JetBrains.Annotations;
using Legacy_do_not_use_it;
using RestAPIWrapper;
using TasksData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public readonly struct SetTaskInEditor
{
    public ITaskData TaskTicket { get; }

    public SetTaskInEditor(ITaskData taskTicket)
    {
        TaskTicket = taskTicket;
    }
}

public class CodeEditor : UIBehaviour
{
    private const string CodeEditorScene = "CodeEditor";

    [SerializeField] private TMP_InputField codeInputField;

    private ITaskData _task;

    private IDisposable _setTaskInEditorUnsubcriber;
    private IDisposable _progLanguageChangedUnsubscriber;

    public static ITaskData Task => Instance._task;

    public static string Code => Instance.codeInputField.text;
    public static ProgLanguage Language { get; private set; }

    private static CodeEditor Instance => FindObjectOfType<CodeEditor>();

    public static async UniTask OpenSolution(ITaskUnit taskFolder)
    {
        Time.timeScale = 0f;
        
        var progress = new ScheduledNotifier<float>();
        progress.ViaLoadingScreen();
        
        var requestProgress = new ScheduledNotifier<float>();
        var openEditorProgress = new ScheduledNotifier<float>();

        requestProgress.Union(openEditorProgress).ReportTo(progress);

        var task = await taskFolder.GetTask(requestProgress);
        await OpenSolution(task, openEditorProgress);
        
        Time.timeScale = 1f;
    }
    
    private static async UniTask OpenSolution(ITaskData task)
    {
        var progress = new ScheduledNotifier<float>();
        progress.ViaLoadingScreen();

        Time.timeScale = 0f;
        
        await OpenSolution(task, progress);
        await WaitWhenEnable();
        
        Time.timeScale = 1f;
    }

    private static async UniTask OpenSolution(ITaskData task, IProgress<float> progress)
    {
        await SceneManager.LoadSceneAsync(CodeEditorScene, LoadSceneMode.Additive).ToUniTask(progress);
        SetTaskInEditor(task);
    }

    private static async UniTask WaitWhenEnable()
    {
        var scene = SceneManager.GetSceneByName(CodeEditorScene);
        await UniTask.WaitWhile(() => scene.isLoaded);
    }

    private static void SetTaskInEditor(ITaskData task)
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

    private void SetDescription(ITaskData task)
    {
        var message = new SetTaskDescriptionMessage(task);
        MessageBroker.Default.Publish(message);
    }

    private void OnProgLanguageChanged(ProgLanguageChanged msg)
    {
        Language = msg.Language;
    }
}