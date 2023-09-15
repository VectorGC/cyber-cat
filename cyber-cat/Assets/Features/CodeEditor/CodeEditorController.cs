using ApiGateway.Client.Internal.Tasks.Verdicts;
using Features.ServerConfig;
using Models;
using TNRD;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeEditorController : UIBehaviour
{
    [SerializeField] private TaskDescription _taskDescription;
    [SerializeField] private SerializableInterface<ICodeEditorView> _codeEditorView;
    [SerializeField] private SerializableInterface<ICodeConsoleView> _console;
    [Header("Buttons")] [SerializeField] private Button _verifySolution;
    [SerializeField] private Button _loadSavedCode;
    [SerializeField] private Button _exit;

    [Header("Debug")] [Tooltip("Enter to play mode, Context -> Load Debug Task")] [SerializeField]
    private TaskType _debugTask;

    private ICodeEditor _codeEditor;

    public void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;

        _taskDescription.Task = _codeEditor.Task;
        _codeEditorView.Value.Language = LanguageProg.Cpp;
    }

#if UNITY_EDITOR
    [MenuItem("CONTEXT/CodeEditorController/Load Debug Task")]
    public static async void LoadTask()
    {
        var controller = FindObjectOfType<CodeEditorController>();
        var taskId = controller._debugTask;

        var player = await ServerAPI.CreatePlayerClient();
        var task = player.Tasks[taskId.GetId()];
        var codeEditor = await CodeEditor.CreateInstance(task);

        controller.Construct(codeEditor);
    }
#endif

    protected override void OnEnable()
    {
        Time.timeScale = 0f;

        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.AddListener(GetSavedCode);
        _exit.onClick.AddListener(ExitEditor);
    }

    protected override void OnDisable()
    {
        Time.timeScale = 1f;

        _verifySolution.onClick.AddListener(VerifySolution);
        _loadSavedCode.onClick.RemoveListener(GetSavedCode);
        _exit.onClick.RemoveListener(ExitEditor);
    }

    private async void VerifySolution()
    {
        var sourceCode = _codeEditorView.Value.SourceCode;
        var verdict = await _codeEditor.Task.VerifySolution(sourceCode);

        switch (verdict)
        {
            case Success success:
                _console.Value.LogSuccess(success.ToString());
                break;
            case Failure failure:
                _console.Value.LogError(failure.ToString());
                break;
        }
    }

    private async void GetSavedCode()
    {
        var sourceCode = await _codeEditor.Task.GetLastSavedSolution();
        _codeEditorView.Value.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        _codeEditor.Close();
    }
}