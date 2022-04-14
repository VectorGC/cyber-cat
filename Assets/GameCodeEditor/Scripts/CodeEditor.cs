using System;
using Authentication;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using GameCodeEditor.Scripts;
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

    public static async UniTaskVoid OpenSolution(ITaskData task, IProgress<float> progress = null)
    {
        Time.timeScale = 0f;
        
        await SceneManager.LoadSceneAsync(CodeEditorScene, LoadSceneMode.Additive).ToUniTask(progress);
        SetTaskInEditor(task);

        await WaitWhenEnable();

        Time.timeScale = 1f;
    }

    private static async UniTask WaitWhenEnable()
    {
        var scene = SceneManager.GetSceneByName(CodeEditorScene);
        await UniTask.WaitWhile(() => scene.isLoaded);
    }

    protected override void OnDestroy()
    {
        var message = new NeedUpdateTaskData(_task.Id, TokenSession.FromPlayerPrefs());
        AsyncMessageBroker.Default.PublishAsync(message);
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