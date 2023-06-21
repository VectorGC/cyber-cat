using ApiGateway.Client;
using Models;
using Shared.Models.Models;
using TNRD;
using UI;
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
    [Header("Debug")] [SerializeField] private string _taskId;

    //private ICodeEditorService _codeEditorService;
    private IJudgeService _judgeService;

    protected override async void Start()
    {
        var authorizationService = AuthorizationServiceFactory.Create(GameManager.ServerUri);
        var token = await authorizationService.Authenticate("cat", "cat");

        var taskRepository = TaskRepositoryFactory.Create(GameManager.ServerUri, token);

        if (!string.IsNullOrEmpty(CodeEditorOpenedTask.TaskId))
        {
            _taskId = CodeEditorOpenedTask.TaskId;
        }

        var task = await taskRepository.GetTask(_taskId);

        _taskDescription.Task = task;
        _codeEditorView.Value.Language = LanguageProg.Cpp;

        _judgeService = JudgeServiceFactory.Create(GameManager.ServerUri, token);
    }

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
        var verdict = await _judgeService.VerifySolution(_taskId, sourceCode);

        switch (verdict.Status)
        {
            case VerdictStatus.Success:
                _console.Value.LogSuccess($"{verdict.Status}: {verdict.TestsPassed} test passed");
                break;
            case VerdictStatus.Failure:
                _console.Value.LogError($"{verdict.Status}: {verdict.TestsPassed} test passed. Error: {verdict.Error}");
                break;
            // TODO: По моему это не надо.
            case VerdictStatus.None:
                _console.Value.Log($"{verdict.Status}: {verdict.TestsPassed} test passed");
                break;
        }
    }

    private async void GetSavedCode()
    {
        var authorizationService = AuthorizationServiceFactory.Create(GameManager.ServerUri);
        var client = new Client(GameManager.ServerUri);

        var token = await authorizationService.Authenticate("cat", "cat");
        client.AddAuthorizationToken(token);

        var sourceCode = await client.GetSavedCode(_taskId);
        _codeEditorView.Value.SourceCode = sourceCode;
    }

    private void ExitEditor()
    {
        new CodeEditorService().CloseEditor().Forget();
    }
}