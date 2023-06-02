using System;
using CodeEditorModels.ProgLanguages;
using Cysharp.Threading.Tasks;
using Legacy_do_not_use_it;
using RestAPIWrapper;
using TaskUnits;
using TaskUnits.Messages;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        await SetTaskInEditor(task);

        await WaitWhenEnable();

        GameMode.Vision = VisionMode.Default;

        Time.timeScale = 1f;
    }

    private static async UniTask WaitWhenEnable()
    {
        var scene = SceneManager.GetSceneByName(CodeEditorScene);
        await UniTask.WaitWhile(() => scene.isLoaded);
    }

    protected override void OnDestroy()
    {
        var token = PlayerPrefs.GetString("token");
        var message = new NeedUpdateTaskData(_task.Id, token);
        AsyncMessageBroker.Default.PublishAsync(message);
    }

    private static async UniTask SetTaskInEditor(ITaskData task)
    {
        var token = PlayerPrefs.GetString("token");
        var lastSavedCode = await RestAPI.Instance.GetLastSavedCode(token, task.Id);

        var message = new SetTaskInEditor(task, lastSavedCode);
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